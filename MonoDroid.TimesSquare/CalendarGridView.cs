﻿using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Graphics;

namespace MonoDroid.TimesSquare
{
    public class CalendarGridView : ViewGroup
    {
        private readonly Paint _dividerPaint = new Paint();

        public CalendarGridView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            _dividerPaint.Color = base.Resources.GetColor(Resource.Color.calendar_divider);
        }

        public override void AddView(View child, int index, LayoutParams @params)
        {
            if (ChildCount == 0) {
                ((CalendarRowView)child).IsHeaderRow = true;
            }
            base.AddView(child, index, @params);
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            var row = (ViewGroup)GetChildAt(1);
            int top = row.Top;
            int bottom = row.Bottom;

            //Left side border.
            int left = row.GetChildAt(0).Left + Left;
            canvas.DrawLine(left, top, left, bottom, _dividerPaint);

            //Each cell's right-side border.
            for (int c = 0; c < 7; c++) {
                int x = left + row.GetChildAt(c).Right - 1;
                canvas.DrawLine(x, top, x, bottom, _dividerPaint);
            }
        }

        protected override bool DrawChild(Canvas canvas, View child, long drawingTime)
        {
            bool isInvalidated = base.DrawChild(canvas, child, drawingTime);
            //Draw a bottom border
            int bottom = child.Bottom - 1;
            canvas.DrawLine(child.Left, bottom, child.Right, bottom, _dividerPaint);
            return isInvalidated;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            long start = DateTime.Now.Millisecond;
            int totalWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int cellSize = totalWidth / 7;
            totalWidth = cellSize * 7;
            int totalHeight = 0;
            int rowWidthSpec = MeasureSpec.MakeMeasureSpec(totalWidth, MeasureSpecMode.Exactly);
            int rowHeightSpec = MeasureSpec.MakeMeasureSpec(cellSize, MeasureSpecMode.Exactly);
            for (int c = 0; c < ChildCount; c++) {
                View child = GetChildAt(c);
                if (child.Visibility == ViewStates.Visible) {
                    MeasureChild(child, rowWidthSpec,
                                 c == 0 ? MeasureSpec.MakeMeasureSpec(cellSize, MeasureSpecMode.AtMost) : rowHeightSpec);
                    totalHeight += child.MeasuredHeight;
                }
            }
            int measuredWidth = totalWidth + 2; // Fudge factor to make the borders show up right.
            SetMeasuredDimension(measuredWidth, totalHeight);
            Logr.D("Grid.OnMeasure {0} ms", DateTime.Now.Millisecond - start);
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            long start = DateTime.Now.Millisecond;
            t = 0;
            for (int c = 0; c < ChildCount; c++) {
                View child = GetChildAt(c);
                int rowHeight = child.MeasuredHeight;
                child.Layout(l, t, r, t + rowHeight);
                t += rowHeight;
            }
            Logr.D("Grid.OnLayout {0} ms", DateTime.Now.Millisecond - start);
        }
    }
}
