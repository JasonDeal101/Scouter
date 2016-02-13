using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Util;

namespace Scouter
{
    public class SlidingTabScrollView : HorizontalScrollView
    {

        private const int TITLE_OFFSET_DIPS = 24;
        private const int TAB_VIEW_PADDING_DIPS = 16;
        private const int TAB_VIEW_TEXT_SIZE_SP = 12;

        private int mTitleOffset;

        private int mTabViewLayoutID;
        private int mTabViewTextViewID;

        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mViewPagerPageChangeListener;

        private static SlidingTabStrip mTabStrip;

        private int mScrollState;

        public interface TabColorizer
        {
            int GetIndicatorColors(int pos);
            int GetDividerColors(int pos);
        }

        //Constuctors
        public SlidingTabScrollView(Context context) : this(context, null) { }
        public SlidingTabScrollView(Context context, IAttributeSet attrs) : this(context, attrs, 0) { }
        public SlidingTabScrollView(Context context, IAttributeSet attrs, int defaultStyle) : base(context, attrs, defaultStyle)
        {
            //Disable the scroll bar
            HorizontalScrollBarEnabled = false;

            //Make sure tab strips fill view
            FillViewport = true;
            this.SetBackgroundColor(Android.Graphics.Color.Gray);

            mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);

            mTabStrip = new SlidingTabStrip(context);
            this.AddView(mTabStrip, LayoutParams.MatchParent, LayoutParams.MatchParent);
        }

        public TabColorizer CustomTabColorizer
        {
            set { mTabStrip.customTabColorizer = value; }
        }

        public int[] SelectedIndicatorColor
        {
            set { mTabStrip.SelectedIndicatorColors = value; }
        }

        public int[] SelectedDividerColor
        {
            set { mTabStrip.DividerColors = value; }
        }

        public ViewPager.IOnPageChangeListener OnPageListner
        {
            set { mViewPagerPageChangeListener = value; }
        }

        public ViewPager ViewPager
        {
            set
            {
                mTabStrip.RemoveAllViews();

                mViewPager = value;
                if (value != null)
                {
                    value.PageSelected += Value_PageSelected;
                    value.PageScrollStateChanged += Value_PageScrollStateChanged;
                    value.PageScrolled += Value_PageScrolled;
                }
            }
        }

        private void Value_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            int tabCount = mTabStrip.ChildCount;

            if((tabCount == 0) || (e.Position < 0) || (e.Position >= tabCount))
            {
                //if any are true return no need to scroll
                return;
            }

            mTabStrip.OnViewPagerPage(e.Position, e.PositionOffset);
        }

        private void Value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Value_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}