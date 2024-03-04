using cv02.Graf;
using cv02.Parser;


namespace cv02
{
    public class GraphProcessor
    {
        public LList LList { get; set; }
        public RList RList { get; set; }

        public GraphData graphData { get; set; }



        public void ProcessGraph(string filePath)
        {
            Parser.Parser parser = new Parser.Parser();
            Data data = parser.loadData(filePath);
            this.graphData = new GraphData(data);

            LList = new LList(graphData);
            RList = new RList(LList);

            LList.printList();
            RList.printList();

            parser.saveData(RList, LList);
        }


    }


}
