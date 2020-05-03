using System.Windows;
using Microsoft.Msagl.Drawing;

namespace Microsoft.Msagl.WpfGraphControl {
    public partial class AutomaticGraphLayoutControl {
        private static readonly DependencyPropertyKey ViewerPropertyKey =
                DependencyProperty.RegisterReadOnly(nameof(Viewer), typeof(IViewer), typeof(AutomaticGraphLayoutControl), new PropertyMetadata());

        public static readonly DependencyProperty ViewerProperty = ViewerPropertyKey.DependencyProperty;

        public static readonly DependencyProperty GraphProperty =
                DependencyProperty.Register(nameof(Graph), typeof(Graph), typeof(AutomaticGraphLayoutControl), new PropertyMetadata(default(Graph),
                    (d, e) => ((AutomaticGraphLayoutControl)d)?.SetGraph()));

        public AutomaticGraphLayoutControl() {
            InitializeComponent();
        }

        public Graph Graph {
            get => (Graph)GetValue(GraphProperty);
            set => SetValue(GraphProperty, value);
        }

        public IViewer Viewer {
            get => (IViewer)GetValue(ViewerProperty);
            private set => SetValue(ViewerPropertyKey, value);
        }

        private void SetGraph() {
            dockPanel.Children.Clear();

            if (Graph is null) {
                return;
            }

            var graphViewer = new GraphViewer();
            graphViewer.BindToPanel(dockPanel);
            graphViewer.Graph = Graph;

            Viewer = graphViewer;
        }
    }
}