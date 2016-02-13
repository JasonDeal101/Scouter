using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace Scouter
{
    public class SlidingTabStrip : LinearLayout
    {
        private const int DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS = 2;
        private const byte DEFAULT_BOTTOM_BORDER_COLOR_ALPHA = 0X26;
        private const int SELECTED_INDICATOR_THICKNESS_DIPS = 8;
        private int[] INDICATOR_COLORS = { 0x19A319, 0x0000FC };
        private int[] DIVIDER_COLORS = { 0xC5C5C5 };

        private const int DEFAULT_DIVIDER_THICKNESS_DIPS = 1;
        private const float DEFAULT_DIVIDER_HEIGHT = 0.5f;

        //Bottom border
        private int mBottomBorderThickness;
        private Paint mBottomBorderPaint;
        private int mDefaultBottomBorderColor;

        //Indicator
        private int mSelectedIndicatorThickness;
        private Paint mSelectedIndicatorPaint;

        //Divider
        private Paint mDividerPaint;
        private float mDividerHeight;

        //Selected position and offset
        private int mSelectedPosition;
        private float mSelectionOffset;

        //Tab colorizer
        private SlidingTabScrollView.TabColorizer mCustomTabColorizer;
        private SimpleTabColorizer mDefaultTabColorizer;

        public SlidingTabStrip(Context context) : this(context, null){ }
        public SlidingTabStrip(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            SetWillNotDraw(false);
            float density = Resources.DisplayMetrics.Density;

            TypedValue outValue = new TypedValue();
            context.Theme.ResolveAttribute(Android.Resource.Attribute.ColorForeground, outValue, true);
            int ThemeForeground = outValue.Data;
            mDefaultBottomBorderColor = SetColorAlpha(ThemeForeground, DEFAULT_BOTTOM_BORDER_COLOR_ALPHA);

            //Sets the colors available for use with the tab bar
            mDefaultTabColorizer = new SimpleTabColorizer();
            mDefaultTabColorizer.IndicatorColors = INDICATOR_COLORS;
            mDefaultTabColorizer.DividerColors = DIVIDER_COLORS;

            mBottomBorderThickness = (int)(DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS * density);
            mBottomBorderPaint = new Paint();
            mBottomBorderPaint.Color = GetColorFromInt(0xC5C5C5);//Gray

            mSelectedIndicatorThickness = (int)(SELECTED_INDICATOR_THICKNESS_DIPS * density);
            mSelectedIndicatorPaint = new Paint();

            mDividerHeight = DEFAULT_DIVIDER_HEIGHT;
            mDividerPaint = new Paint();
            mDividerPaint.StrokeWidth = (int)(DEFAULT_DIVIDER_THICKNESS_DIPS * density);

        }

        public SlidingTabScrollView.TabColorizer customTabColorizer
        {
            set
            {
                mCustomTabColorizer = value;
                this.Invalidate();//Forces view to draw
            }
        }

        public int[] SelectedIndicatorColors
        {
            set
            {
                mCustomTabColorizer = null;
                mDefaultTabColorizer.IndicatorColors = value;
                this.Invalidate();
            }
        }

        public int[] DividerColors
        {
            set
            {
                mCustomTabColorizer = null;
                mDefaultTabColorizer.DividerColors = value;
                this.Invalidate();
            }
        }

        private Color GetColorFromInt(int color)
        {
            //Will return a color value readable by android from hexadecimal color value
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

        private int SetColorAlpha(int color, byte alpha)
        {
            //Same as color from int but includes the alpha value
            return Color.Argb(alpha, Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

        public void OnViewPagerPage(int pos, float posOffset)
        {
            mSelectedPosition = pos;
            mSelectionOffset = posOffset;
            this.Invalidate();//Calls OnDraw
        }

        protected override void OnDraw(Canvas canvas)
        {
            int height = Height;
            int tabCount = ChildCount;
            int dividerHeightPx = (int)(Math.Min(Math.Max(0f, mDividerHeight), 1f) * height);
            SlidingTabScrollView.TabColorizer tabColor = mCustomTabColorizer != null ? mCustomTabColorizer : mDefaultTabColorizer;

            //Thick colored underline below selected tab
            if (tabCount > 0)
            {
                View selectedTitle = GetChildAt(mSelectedPosition);
                int left = selectedTitle.Left;
                int right = selectedTitle.Right;
                int color = tabColor.GetIndicatorColors(mSelectedPosition);

                if(mSelectionOffset > 0f && mSelectedPosition < (tabCount - 1))
                {
                    int nextColor = tabColor.GetIndicatorColors(mSelectedPosition + 1);
                    if(color != nextColor)
                    {
                        color = blendColors(color, nextColor, mSelectionOffset);
                    }
                    View nextTitle = GetChildAt(mSelectedPosition + 1);
                    left = (int)(mSelectionOffset * nextTitle.Left + (1.0f - mSelectionOffset) * left);
                    right = (int)(mSelectionOffset * nextTitle.Right + (1.0f - mSelectionOffset) * left);
                }
                mSelectedIndicatorPaint.Color = GetColorFromInt(color);

                canvas.DrawRect(left, height - mSelectedIndicatorThickness, right, height, mSelectedIndicatorPaint);

                //Create vertical dividers between tabs
                int seperatorTop = (height - dividerHeightPx) / 2;
                for(int i = 0; i < tabCount; i++)
                {
                    View tab = GetChildAt(i);
                    mDividerPaint.Color = GetColorFromInt(tabColor.GetDividerColors(i));
                    canvas.DrawLine(tab.Right, seperatorTop, tab.Right, seperatorTop + dividerHeightPx, mDividerPaint);
                }
                canvas.DrawRect(0, height - mBottomBorderThickness, Width, height, mBottomBorderPaint);
            }
        }

        private int blendColors(int color, int nextColor, float ratio)
        {
            float inverseRatio = 1f - ratio;
            float r = (Color.GetRedComponent(color) * ratio) + (Color.GetRedComponent(nextColor) * inverseRatio);
            float g = (Color.GetGreenComponent(color) * ratio) + (Color.GetGreenComponent(nextColor) * inverseRatio);
            float b = (Color.GetBlueComponent(color) * ratio) + (Color.GetBlueComponent(nextColor) * inverseRatio);
            return Color.Rgb((int)r, (int)g, (int)b);
        }

        public class SimpleTabColorizer : SlidingTabScrollView.TabColorizer
        {
            //Used to get and set an array of colors for use with the tab bar
            private int[] dividerColors;
            private int[] indicatorColors;

            public int GetIndicatorColors(int pos)
            {
                return indicatorColors[pos % indicatorColors.Length];
            }

            public int GetDividerColors(int pos)
            {
                return dividerColors[pos % indicatorColors.Length];
            }

            public int[] IndicatorColors
            {
                set { indicatorColors = value; }
            }

            public int[] DividerColors
            {
                set { dividerColors = value; }
            }
        }
    }
}