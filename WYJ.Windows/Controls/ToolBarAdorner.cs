using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WYJ.Windows.Controls
{
    public class ToolBarAdorner : Adorner
    {
        public ToolBarAdorner(UIElement parent)
            : base(parent)
        {

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (AdornedElement != null)
            {
                Rect rect = new Rect(100, 100, (AdornedElement as FrameworkElement).ActualWidth, 30);
                drawingContext.PushOpacity(1.0);
                drawingContext.DrawRectangle(new VisualBrush(Child), new Pen(Brushes.Transparent, 0), rect);
                drawingContext.Pop();
            }
        }
        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register("Child", typeof(UIElement), typeof(ToolBarAdorner), new PropertyMetadata(null));
    }
}
