namespace ImageSort
{
    public partial class Form1 : Form
    {

        struct dot
        {
            public int R;
            public int G;
            public int B;
            public double Bright;
        }

        public Form1()
        {
            InitializeComponent();
        }

        static List<dot> MergeLists(List<dot> L1, List<dot> L2)
        {
            List<dot> mergedList = new List<dot>();
            int indexLeft = 0, indexRight = 0;
            while (indexLeft < L1.Count || indexRight < L2.Count)
            {
                if (indexLeft < L1.Count && indexRight < L2.Count)
                {
                    if (L1[indexLeft].Bright <= L2[indexRight].Bright)
                    {
                        mergedList.Add(L1[indexLeft]);
                        indexLeft++;
                    }
                    else
                    {
                        mergedList.Add(L2[indexRight]);
                        indexRight++;
                    }
                }
                else if (indexLeft < L1.Count)
                {
                    mergedList.Add(L1[indexLeft]);
                    indexLeft++;
                }
                else if (indexRight < L2.Count)
                {
                    mergedList.Add(L2[indexRight]);
                    indexRight++;
                }
            }

            return mergedList;
        }

        static List<dot> MergeSort(List<dot> ListToSort)
        {
            List<dot> SortedList = new List<dot>();
            List<dot> leftList = new List<dot>();
            List<dot> rightList = new List<dot>();
            int half = ListToSort.Count / 2;
            for (int i = 0; i < half; i++)
            {
                leftList.Add(ListToSort[i]);
            }
            for (int i = half; i < ListToSort.Count; i++)
            {
                rightList.Add(ListToSort[i]);
            }
            if (leftList.Count > 1)
                leftList = MergeSort(leftList);
            if (rightList.Count > 1)
                rightList = MergeSort(rightList);

            SortedList = MergeLists(leftList, rightList);
            return SortedList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap image = (Bitmap)pictureBox1.Image;

            int width = image.Width; int height = image.Height;

            List<dot> bright = ChangeToBrightness(image);
            List<dot> orderd = MergeSort(bright);

            Bitmap sortedimage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    dot pixel = orderd[(y * width) + x];
                    Color newColor = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                    sortedimage.SetPixel(x, y, newColor);
                }
            }
            pictureBox2.Image = sortedimage;
        }

        private List<dot> ChangeToBrightness(Bitmap image)
        {
            List<dot> brightness = new List<dot>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    double brightnessvalue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    dot pixel = new dot();
                    pixel.R = pixelColor.R; pixel.G = pixelColor.G; pixel.B = pixelColor.B;
                    pixel.Bright = brightnessvalue;

                    brightness.Add(pixel);
                }
            }
            return brightness;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files |*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}