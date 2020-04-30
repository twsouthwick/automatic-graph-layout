using System;
using System.Windows;
using Microsoft.Msagl.Drawing;

namespace Microsoft.Msagl.WpfGraphControl {
    public partial class AutomaticGraphLayoutControl {
        public AutomaticGraphLayoutControl() {
            InitializeComponent();
            Loaded += (s, e) => SetGraph();
        }
        public Graph Graph {
            get => (Graph)GetValue(GraphProperty);
            set => SetValue(GraphProperty, value);
        }
        public static readonly DependencyProperty GraphProperty =
            DependencyProperty.Register("Graph", typeof(Graph), typeof(AutomaticGraphLayoutControl), new PropertyMetadata(default(Graph),
                (d, e) => ((AutomaticGraphLayoutControl)d)?.SetGraph()));

        private void SetGraph() {

            if (Graph is null) {
                dockPanel.Children.Clear();
                return;
            }

            GraphViewerRemoved?.Invoke(this, _viewer);

            _viewer = new GraphViewer();
            _viewer.BindToPanel(dockPanel);
            _viewer.Graph = Graph;

            GraphViewerAdded?.Invoke(this, _viewer);
        }

        private GraphViewer _viewer;

        public event EventHandler<GraphViewer> GraphViewerRemoved;
        public event EventHandler<GraphViewer> GraphViewerAdded;
    }
}