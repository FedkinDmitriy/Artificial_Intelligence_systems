using AI_lab1.Lib;

namespace AI_lab1
{
    public partial class FormMain : Form
    {
        #region System Values and Ctors
        private Button[,] cells = new Button[8, 8];
        private States currentState = States.Empty;
        private (int x, int y)? knightPos = null; //Агент
        private (int x, int y)? kingPos = null;
        private States[,] gridStates = new States[8, 8]; //Среда    
        public FormMain()
        {
            InitializeComponent();           
        }
        #endregion System Values and Ctors

        private void FormMain_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var btn = new Button
                    {
                        Dock = DockStyle.Fill,
                        BackColor = (i + j) % 2 == 0 ? Color.GhostWhite : Color.SaddleBrown,
                        Margin = new Padding(0),
                        Tag = (i, j) // сохраняем координаты прямо в кнопку
                    };

                    btn.Click += (s, eArgs) =>
                    {
                        var button = (Button)s!;
                        var (x, y) = ((int, int))button.Tag!;

                        switch (currentState)
                        {
                            case States.Knight:
                                {
                                    if (knightPos != null)
                                    {
                                        var (px, py) = knightPos.Value;
                                        cells[px, py].Image = null;
                                        cells[px, py].Text = "";
                                    }
                                    //button.Text = "Knight";
                                    //button.ForeColor = Color.Black;
                                    button.Image = Properties.Resources.Knight50;
                                    button.ImageAlign = ContentAlignment.MiddleCenter;
                                    button.Text = "";
                                    knightPos = (x, y);
                                    break;
                                }
                            case States.King:
                                {
                                    if (kingPos != null)
                                    {
                                        var (px, py) = kingPos.Value;
                                        cells[px, py].Image = null;
                                        cells[px, py].Text = "";
                                    }
                                    //button.Text = "King";
                                    //button.ForeColor = Color.Blue;
                                    button.Image = Properties.Resources.King50;
                                    button.ImageAlign = ContentAlignment.MiddleCenter;
                                    button.Text = "";
                                    kingPos = (x, y);
                                    break;
                                }
                            case States.Burning:
                                {
                                    button.Text = "";
                                    button.BackColor = Color.Red;
                                    gridStates[x, y] = States.Burning;
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show($"Нажата клетка: {x + 1},{y + 1}");
                                    break;
                                }
                        }
                    };

                    cells[i, j] = btn;
                    tableLayoutPanel1.Controls.Add(btn, j, i); // (колонка, строка)
                }
            }
        }


        private void buttonKnight_Click(object sender, EventArgs e)
        {
            currentState = States.Knight;
        }
        private void buttonKing_Click(object sender, EventArgs e)
        {
            currentState = States.King;
        }
        private void buttonBurn_Click(object sender, EventArgs e)
        {
            currentState = States.Burning;
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (knightPos == null || kingPos == null)
            {
                MessageBox.Show("Сначала разместите коня и короля!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solver = new Solver(gridStates);

            // путь к королю
            var pathToKing = solver.FindPath(knightPos.Value, kingPos.Value);
            if (pathToKing == null)
            {
                MessageBox.Show("Конь не может дойти до короля!","Внимание!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // отмечаем клетки как пройденные, пропускаем первую (где находился конь)
            foreach (var node in pathToKing.Skip(1))
            {
                if (gridStates[node.X, node.Y] == States.Empty) gridStates[node.X, node.Y] = States.Visited;
            }

            // путь обратно
            var pathBack = solver.FindPath(kingPos.Value, knightPos.Value);
            if (pathBack == null)
            {
                MessageBox.Show("Конь дошёл до короля, но не может вернуться, не заходя на пройденные клетки!", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // подсчет шагов
            int totalSteps = (pathToKing.Count - 1) + (pathBack.Count - 1);
            MessageBox.Show(
                $"Путь найден!\nШагов до короля: {pathToKing.Count - 1}\n" +
                $"Шагов обратно: {pathBack.Count - 1}\nВсего шагов: {totalSteps}",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );


            // визуализация пути
            await HighlightPathAsync(pathToKing, Color.LightBlue);
            await HighlightPathAsync(pathBack, Color.LightGreen);
        }

        private async Task HighlightPathAsync(List<Node> path, Color color, bool markVisited = true)
        {
            foreach (var node in path)
            {
                var btn = cells[node.X, node.Y];
                var original = btn.BackColor;

                btn.BackColor = color;
                await Task.Delay(500); // пауза 500 мс

                if (markVisited)
                {
                    btn.BackColor = Color.Gray;
                }
                else
                {
                    btn.BackColor = original;
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            knightPos = null;
            kingPos = null;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    gridStates[i, j] = States.Empty;
                    cells[i, j].Image = null;
                    cells[i, j].Text = "";
                    cells[i, j].BackColor = (i + j) % 2 == 0 ? Color.GhostWhite : Color.SaddleBrown;
                }
            }
            currentState = States.Empty;
        }

    }
}
