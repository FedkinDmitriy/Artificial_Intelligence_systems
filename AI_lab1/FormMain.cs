using AI_lab1.Lib;
using System.Media;

namespace AI_lab1
{
    public partial class FormMain : Form
    {
        #region System Values and Ctors
        private Button[,] cells = new Button[8, 8];
        private States currentState = States.Empty;
        private (int x, int y)? knightPos = null; //�����
        private (int x, int y)? kingPos = null;
        private States[,] gridStates = new States[8, 8]; //�����    
        private SoundPlayer? player;
        private bool isPlaying = false;
        public FormMain()
        {
            InitializeComponent();
            checkBoxBFS.Checked = true;
            checkBoxDFS.Checked = false;
        }
        #endregion System Values and Ctors

        private void FormMain_Load(object sender, EventArgs e)
        {
            player = new SoundPlayer(Properties.Resources.soundAttila);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var btn = new Button
                    {
                        Dock = DockStyle.Fill,
                        BackColor = (i + j) % 2 == 0 ? Color.GhostWhite : Color.SaddleBrown,
                        Margin = new Padding(0),
                        Tag = (i, j) // ��������� ���������� ����� � ������
                    };

                    btn.Click += (s, eArgs) =>
                    {
                        var button = s as Button;

                        if (button is null) return;

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
                                    //MessageBox.Show($"������ ������: {x + 1},{y + 1}");
                                    break;
                                }
                        }
                    };

                    cells[i, j] = btn;
                    tableLayoutPanel1.Controls.Add(btn, j, i); // (�������, ������)
                }
            }
        }

        #region Switch states env
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
        #endregion Switch states env

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (knightPos == null || kingPos == null)
            {
                MessageBox.Show("������� ���������� ���� � ������!", "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solver = new Solver(gridStates);

            //Func<(int x, int y), (int x, int y), List<Node>?>? findMethod = null;
            //if (checkBoxBFS.Checked && !checkBoxDFS.Checked && !checkBoxIterDFS.Checked)
            //{
            //    findMethod = solver.FindPathBFS;
            //}
            //else if (checkBoxDFS.Checked && !checkBoxBFS.Checked && !checkBoxIterDFS.Checked)
            //{
            //    findMethod = solver.FindPathDFS;
            //}
            //else
            //{
            //    MessageBox.Show("�������� ����� ������ (BFS ��� DFS)!", "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    Reset();
            //    return;
            //}

            // ���� � ������
            var pathToKing = RunSolver(solver, knightPos.Value, kingPos.Value);
            //var pathToKing = findMethod(knightPos.Value, kingPos.Value);
            if (pathToKing == null)
            {
                MessageBox.Show("���� �� ����� ����� �� ������!", "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // �������� ������ ��� ����������, ���������� ������ (�� ������� ����� ����)
            foreach (var node in pathToKing.Skip(1))
            {
                if (gridStates[node.X, node.Y] == States.Empty)
                    gridStates[node.X, node.Y] = States.Visited;
            }

            // ���� �������
            var pathBack = RunSolver(solver,kingPos.Value, knightPos.Value);
            //var pathBack = findMethod(kingPos.Value, knightPos.Value);
            if (pathBack == null)
            {
                MessageBox.Show("���� ����� �� ������, �� �� ����� ���������!", "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ������� �����
            textBoxIterations.Text = solver.Iterations.ToString();
            textBoxStates.Text = solver.GeneratedStates.ToString();
            textBoxMemory.Text = solver.MaxOpenCount.ToString();
            int totalSteps = (pathToKing.Count - 1) + (pathBack.Count - 1);
            MessageBox.Show(
                $"���� ������!\n����� �� ������: {pathToKing.Count - 1}\n" +
                $"����� �������: {pathBack.Count - 1}\n����� �����: {totalSteps}\n",
                "����������", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            // ������������ ����
            await HighlightPathAsync(pathToKing, Color.LightBlue);
            await HighlightPathAsync(pathBack, Color.LightGreen);
        }
        private void Reset()
        {
            textBoxStates.Clear();
            textBoxIterations.Clear();
            textBoxMemory.Clear();
            player?.Stop();
            isPlaying = false;
            buttonSound.Text = "���. ����";
            checkBoxBFS.Checked = false;
            checkBoxDFS.Checked = false;
            checkBoxIterDFS.Checked = false;
            knightPos = null;
            kingPos = null;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    gridStates[i, j] = States.Empty;
                    cells[i, j].Image = null;
                    cells[i, j].Text = "";
                    cells[i, j].BackColor = (i + j) % 2 == 0 ? Color.GhostWhite : Color.SaddleBrown;
                }
            currentState = States.Empty;
        }
        private async Task HighlightPathAsync(List<Node> path, Color color, bool markVisited = true)
        {
            foreach (var node in path)
            {
                var btn = cells[node.X, node.Y];
                var original = btn.BackColor;

                btn.BackColor = color;
                await Task.Delay(500); // ����� 500 ��

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
            Reset();
        }
        private void buttonSound_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                player?.PlayLooping();
                isPlaying = true;
                buttonSound.Text = "����. ����";
            }
            else
            {
                player?.Stop();
                isPlaying = false;
                buttonSound.Text = "���. ����";
            }
        }
        private List<Node>? RunSolver(Solver solver,(int x, int y) start,(int x, int y) target)
        {
            if (checkBoxBFS.Checked && !checkBoxDFS.Checked && !checkBoxIterDFS.Checked && !checkBoxBiSearch.Checked)
            {
                return solver.FindPathBFS(start, target);
            }
            else if (!checkBoxBFS.Checked && checkBoxDFS.Checked && !checkBoxIterDFS.Checked && !checkBoxBiSearch.Checked)
            {
                return solver.FindPathDFS(start, target);
            }
            else if (!checkBoxBFS.Checked && !checkBoxDFS.Checked && checkBoxIterDFS.Checked && !checkBoxBiSearch.Checked)
            {
                int maxDepth = 64;
                return solver.FindPathIterativeDeepening(start, target, maxDepth);
            }
            else if(!checkBoxBFS.Checked && !checkBoxDFS.Checked && !checkBoxIterDFS.Checked && checkBoxBiSearch.Checked)
            {
                return solver.FindPathBidirectionalBFS(start, target);
            }
            else
            {
                MessageBox.Show("�������� ����� ������!", "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Reset();
            }
            return null;
        }

    }
}
