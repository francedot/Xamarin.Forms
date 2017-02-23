using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Flex;

namespace Xamarin.Forms
{
	public partial class FlexLayout : Layout<View>
	{
		static readonly BindableProperty FlexNodeProperty = BindableProperty.CreateAttached(_FlexNodePropertyName, typeof(IFlexNode), typeof(FlexLayout), null);

		public static readonly BindableProperty IsIncludedProperty = BindableProperty.CreateAttached(_FlexIsIncludedPropertyName, typeof(bool), typeof(FlexLayout), true);

		public static readonly BindableProperty OrderProperty = BindableProperty.CreateAttached(_FlexOrderPropertyName, typeof(int), typeof(FlexLayout), -1);

		public static readonly BindableProperty FlexProperty = BindableProperty.CreateAttached(_FlexPropertyName, typeof(float), typeof(FlexLayout), default(float), propertyChanging: (s, o, n) => { if (GetNode((View)s) == null) return; GetNode((View)s).Flex = (float)n; });

		public static readonly BindableProperty GrowProperty = BindableProperty.CreateAttached(_FlexGrowPropertyName, typeof(float), typeof(FlexLayout), default(float), propertyChanging: (s, o, n) => { if (GetNode((View)s) == null) return; GetNode((View)s).FlexGrow = (float)n; });

		public static readonly BindableProperty ShrinkProperty = BindableProperty.CreateAttached(_FlexShrinkPropertyName, typeof(float), typeof(FlexLayout), -1f, propertyChanging: (s, o, n) => { if (GetNode((View)s) == null) return; GetNode((View)s).FlexShrink = (float)n; });

		public static readonly BindableProperty BasisProperty = BindableProperty.CreateAttached(_FlexBasisPropertyName, typeof(float), typeof(FlexLayout), float.NaN, propertyChanging: (s, o, n) => { if (GetNode((View)s) == null) return; GetNode((View)s).FlexBasis = (float)n; });

		public static readonly BindableProperty AlignSelfProperty = BindableProperty.CreateAttached(_FlexAlignSelfPropertyName, typeof(Align), typeof(FlexLayout), default(Align), propertyChanging: (s, o, n) =>
		{
			if (GetNode((View)s) == null) return;
			GetNode((View)s).AlignSelf = (Align)n;
		});

		public static readonly BindableProperty FlexDirectionProperty = BindableProperty.Create(nameof(FlexDirection), typeof(FlexDirection), typeof(FlexLayout), FlexDirection.Row, propertyChanging: (s, o, n) =>
		{
			GetNode((View)s).FlexDirection = (FlexDirection)n;
			(s as FlexLayout).InvalidateLayout();
		});

		public static readonly BindableProperty JustifyContentProperty = BindableProperty.Create(nameof(JustifyContent), typeof(Justify), typeof(FlexLayout), Justify.FlexStart, propertyChanging: (s, o, n) => { GetNode((View)s).JustifyContent = (Justify)n; (s as FlexLayout).InvalidateLayout(); });

		public static readonly BindableProperty AlignContentProperty = BindableProperty.Create(nameof(AlignContent), typeof(Align), typeof(FlexLayout), Align.FlexStart, propertyChanging: (s, o, n) => { GetNode((View)s).AlignContent = (Align)n; (s as FlexLayout).InvalidateLayout(); (s as FlexLayout).InvalidateLayout(); });

		public static readonly BindableProperty AlignItemsProperty = BindableProperty.Create(nameof(AlignItems), typeof(Align), typeof(FlexLayout), Align.Stretch, propertyChanging: (s, o, n) => { GetNode((View)s).AlignItems = (Align)n; (s as FlexLayout).InvalidateLayout(); });

		public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(Position), typeof(FlexLayout), Position.Relative, propertyChanging: (s, o, n) => { GetNode((View)s).PositionType = (Position)n; (s as FlexLayout).InvalidateLayout(); });

		public static readonly BindableProperty OverflowProperty = BindableProperty.Create(nameof(Overflow), typeof(Overflow), typeof(FlexLayout), Overflow.Visible, propertyChanging: (s, o, n) => { GetNode((View)s).Overflow = (Overflow)n; (s as FlexLayout).InvalidateLayout(); });

		public static readonly BindableProperty WrapProperty = BindableProperty.Create(nameof(Wrap), typeof(Wrap), typeof(FlexLayout), Wrap.NoWrap, propertyChanging: (s, o, n) => { GetNode((View)s).Wrap = (Wrap)n; (s as FlexLayout).InvalidateLayout(); });

		public static bool GetIsIncluded(BindableObject bindable)
		{
			return (bool)bindable.GetValue(IsIncludedProperty);
		}

		public static void SetIsIncluded(BindableObject bindable, bool value)
		{
			bindable.SetValue(IsIncludedProperty, value);
		}

		public static void SetGrow(BindableObject bindable, float value)
		{
			bindable.SetValue(GrowProperty, value);
		}

		public static float GetGrow(BindableObject bindable)
		{
			return (float)bindable.GetValue(GrowProperty);
		}

		public static void SetShrink(BindableObject bindable, float value)
		{
			bindable.SetValue(ShrinkProperty, value);
		}

		public static float GetShrink(BindableObject bindable)
		{
			return (float)bindable.GetValue(ShrinkProperty);
		}

		public static void SetBasis(BindableObject bindable, float value)
		{
			bindable.SetValue(BasisProperty, value);
		}

		public static float GetBasis(BindableObject bindable)
		{
			return (float)bindable.GetValue(BasisProperty);
		}

		public static void SetFlex(BindableObject bindable, float value)
		{
			bindable.SetValue(FlexProperty, value);
		}

		public static float GetFlex(BindableObject bindable)
		{
			return (float)bindable.GetValue(FlexProperty);
		}

		public static void SetAlignSelf(BindableObject bindable, Align value)
		{
			bindable.SetValue(AlignSelfProperty, value);
		}

		public static Align GetAlignSelf(BindableObject bindable)
		{
			return (Align)bindable.GetValue(AlignSelfProperty);
		}

		public static void SetOrder(BindableObject bindable, int value)
		{
			bindable.SetValue(OrderProperty, value);
		}

		public static int GetOrder(BindableObject bindable)
		{
			return (int)bindable.GetValue(OrderProperty);
		}

		public FlexDirection FlexDirection
		{
			get { return (FlexDirection)GetValue(FlexDirectionProperty); }
			set { SetValue(FlexDirectionProperty, value); }
		}

		public Justify JustifyContent
		{
			get { return (Justify)GetValue(JustifyContentProperty); }
			set { SetValue(JustifyContentProperty, value); }
		}

		public Align AlignContent
		{
			get { return (Align)GetValue(AlignContentProperty); }
			set { SetValue(AlignContentProperty, value); }
		}

		public Align AlignItems
		{
			get { return (Align)GetValue(AlignItemsProperty); }
			set { SetValue(AlignItemsProperty, value); }
		}

		public Position Position
		{
			get { return (Position)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public Wrap Wrap
		{
			get { return (Wrap)GetValue(WrapProperty); }
			set { SetValue(WrapProperty, value); }
		}

		public Overflow Overflow
		{
			get { return (Overflow)GetValue(OverflowProperty); }
			set { SetValue(OverflowProperty, value); }
		}

		protected override void OnChildAdded(Element child)
		{
			base.OnChildAdded(child);
			RegisterChild(child as View);
		}

		protected override void OnChildRemoved(Element child)
		{
			base.OnChildRemoved(child);
			UnregisterChild(child as View);
		}

		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			ApplyLayout(x, y, width, height);
		}

		protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
		{
			var requestSize = CalculateLayoutWithSize((float)widthConstraint, (float)heightConstraint);
			return new SizeRequest(requestSize);
		}

		protected override void InvalidateMeasure()
		{
			base.InvalidateMeasure();
		}

		static IFlexNode GetNode(View bindable)
		{
			return (IFlexNode)bindable.GetValue(FlexNodeProperty);
		}

		static void SetNode(View bindable, object node)
		{
			bindable.SetValue(FlexNodeProperty, node);
		}

		void UpdateRootNode()
		{
			//set initial values, this could be != from default of the engine
			_root.FlexDirection = FlexDirection;
			_root.JustifyContent = JustifyContent;
			_root.AlignContent = AlignContent;
			_root.AlignSelf = GetAlignSelf(this);
			_root.AlignItems = AlignItems;
			_root.Overflow = Overflow;
			_root.PositionType = Position;
			_root.Wrap = Wrap;
			_root.FlexGrow = GetGrow(this);
			_root.FlexBasis = GetBasis(this);
			_root.FlexShrink = GetShrink(this);

		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			UpdateNode(this, this, propertyName);
		}

		void AttachNodesFromViewHierachy(View view)
		{
			var node = GetNode(view);
			// Only leaf nodes should have a measure function
			if (view.IsLeaf())
			{
				node.Clear();
				node.SetMeasure(MeasureView);
				return;
			}

			node.SetMeasure(null);

			// Create a list of all the subviews that we are going to use for layout.
			var subviewsToInclude = new List<View>();
			foreach (var subview in FlexLayoutExtensions.GetChildren(view))
			{
				if (GetIsIncluded(subview))
				{
					subviewsToInclude.Add(subview);
				}
			}

			if (!NodeHasExactSameChildren(node, subviewsToInclude.ToArray()))
			{
				node.Clear();
				for (int i = 0; i < subviewsToInclude.Count; i++)
				{
					var subView = subviewsToInclude[i];
					var subViewNode = GetNode(subView);
					subViewNode.MarginLeft = (float)subView.Margin.Left;
					subViewNode.MarginTop = (float)subView.Margin.Top;
					subViewNode.MarginRight = (float)subView.Margin.Right;
					subViewNode.MarginBottom = (float)subView.Margin.Bottom;
					subViewNode.FlexGrow = GetGrow(subView);
					subViewNode.FlexBasis = GetBasis(subView);
					subViewNode.FlexShrink = GetShrink(subView);
					subViewNode.AlignSelf = GetAlignSelf(subView);
					subViewNode.Flex = GetFlex(subView);
					node.Insert(i, subViewNode);
				}
			}

			foreach (var subView in subviewsToInclude)
				AttachNodesFromViewHierachy(subView);

		}

		Size MeasureView(IFlexNode node, float width, FlexMeasureMode widthMode, float height, FlexMeasureMode heightMode)
		{
			var constrainedWidth = (widthMode == FlexMeasureMode.None) ? float.MaxValue : width;
			var constrainedHeight = (heightMode == FlexMeasureMode.None) ? float.MaxValue : height;

			View view = null;
			if (FlexLayoutExtensions.Bridges.ContainsKey(node))
				view = FlexLayoutExtensions.Bridges[node] as View;

			var sizeRequest = view.Measure(constrainedWidth, constrainedHeight);

			var sizeThatFitsWidth = (float)sizeRequest.Request.Width;
			var sizeThatFitsHeight = (float)sizeRequest.Request.Height;

			var finalWidth = SanitizeMeasurement(constrainedWidth, sizeThatFitsWidth, widthMode);
			var finalHeight = SanitizeMeasurement(constrainedHeight, sizeThatFitsHeight, heightMode);

			return new Size(finalWidth, finalHeight);
		}

		static void UpdateNode(FlexLayout parent, object element, string propertyName)
		{
			bool shouldInvalidate = false;
			if (propertyName.Equals(WidthRequestProperty.PropertyName) ||
				propertyName.Equals(HeightRequestProperty.PropertyName) ||
				propertyName.Equals(MinimumHeightRequestProperty.PropertyName) ||
				propertyName.Equals(MinimumWidthRequestProperty.PropertyName) ||
				propertyName.Equals(_FlexAlignSelfPropertyName))
			{
				var view = element as View;
				if (view == null)
					throw new ArgumentNullException(nameof(element));

				var node = GetNode(view);
				node.Width = (float)view.WidthRequest;
				node.Height = (float)view.HeightRequest;
				shouldInvalidate = true;
			}

			if (propertyName.Equals(_FlexAlignSelfPropertyName) ||
				propertyName.Equals(_FlexBasisPropertyName) ||
				propertyName.Equals(_FlexGrowPropertyName) ||
				propertyName.Equals(_FlexShrinkPropertyName) ||
				propertyName.Equals(_FlexIsIncludedPropertyName) ||
				propertyName.Equals(_FlexOrderPropertyName) ||
				propertyName.Equals(_FlexPropertyName)
				)
			{
				shouldInvalidate = true;
			}

			if (shouldInvalidate)
				parent.InvalidateLayout();
		}

		static void ApplyLayoutToNativeView(View view, IFlexNode node)
		{
			var topLeft = new Point(node.LayoutLeft, node.LayoutTop);
			var size = new Size(node.LayoutWidth, node.LayoutHeight);
			view.Layout(new Rectangle(topLeft, size));
		}
	}
}
