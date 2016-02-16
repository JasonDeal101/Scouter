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

            View SelectedTitle = mTabStrip.GetChildAt(e.Position);

            int extraOffset = (SelectedTitle != null ? (int)(e.Position * SelectedTitle.Width) : 0);

            ScrollToTab(e.Position, extraOffset);

            if(mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrolled(e.Position, e.PositionOffset, e.PositionOffsetPixels);
            }
        }

        private void Value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            mScrollState = e.State;

            if(mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrollStateChanged(e.State);
            }
        }

        private void Value_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if(mScrollState == ViewPager.ScrollStateIdle)
            {
                mTabStrip.OnViewPagerPage(e.Position, 0f);
                ScrollToTab(e.Position, 0);
            }
            if(mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageSelected(e.Position);
            }
        }

        private void PopulateTabStrip()
        {
            PagerAdapter adapter = mViewPager.Adapter;
            for(int i = 0;  i < adapter.Count; i++)
            {
                TextView tabView = CreateDefaultTabView(Context);
                tabView.Text = i.ToString();
                tabView.SetTextColor(Android.Graphics.Color.Black);
                tabView.Tag = i;
                tabView.Click += TabView_Click;
                mTabStrip.AddView(tabView);
            }
        }

        private void TabView_Click(object sender, EventArgs e)
        {
            TextView clickTab = (TextView)sender;
            int pageToScrollTo = (int)clickTab.Tag;
            mViewPager.CurrentItem = pageToScrollTo;
        }

        private TextView CreateDefaultTabView(Context context)
        {
            throw new NotImplementedException();
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if(mViewPager != null)
            {
                ScrollToTab(mViewPager.CurrentItem, 0);
            }
        }

        private void ScrollToTab(int pos, int offset)
        {
            int tabCount = mTabStrip.ChildCount;

            if (tabCount == 0 || pos < 0 || pos >= tabCount)
            {
                //No need to go further
                return;
            }

            View selectedChild = mTabStrip.GetChildAt(pos);
            if(selectedChild != null)
            {
                int scrollAmountX = selectedChild.Left + offset;

                if(pos > 0 || offset > 0)
                {
                    scrollAmountX -= mTitleOffset;
                }

                this.ScrollTo(scrollAmountX, 0);
            }
        }
    }
}