namespace RekenmachineWinForms.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblDisplay = new Label();
        this.lblExpression = new Label();
        this.listHistory = new ListBox();
        this.lblHistoryTitle = new Label();

        // Digit buttons
        this.btn0 = new Button();
        this.btn1 = new Button();
        this.btn2 = new Button();
        this.btn3 = new Button();
        this.btn4 = new Button();
        this.btn5 = new Button();
        this.btn6 = new Button();
        this.btn7 = new Button();
        this.btn8 = new Button();
        this.btn9 = new Button();
        this.btnDecimal = new Button();

        // Operator buttons
        this.btnAdd = new Button();
        this.btnSubtract = new Button();
        this.btnMultiply = new Button();
        this.btnDivide = new Button();
        this.btnEquals = new Button();

        // Function buttons
        this.btnClear = new Button();
        this.btnBackspace = new Button();
        this.btnToggleSign = new Button();
        this.btnPercent = new Button();

        this.SuspendLayout();

        // Form
        this.Text = "Rekenmachine";
        this.Size = new Size(520, 560);
        this.MinimumSize = new Size(520, 560);
        this.MaximumSize = new Size(520, 560);
        this.BackColor = Color.FromArgb(30, 30, 30);
        this.ForeColor = Color.White;
        this.Font = new Font("Segoe UI", 10F);
        this.KeyPreview = true;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Expression label (shows "12 + 5 =")
        this.lblExpression.Size = new Size(300, 24);
        this.lblExpression.Location = new Point(10, 10);
        this.lblExpression.ForeColor = Color.FromArgb(160, 160, 160);
        this.lblExpression.Font = new Font("Segoe UI", 11F);
        this.lblExpression.TextAlign = ContentAlignment.MiddleRight;
        this.lblExpression.Text = "";

        // Display label
        this.lblDisplay.Size = new Size(300, 60);
        this.lblDisplay.Location = new Point(10, 36);
        this.lblDisplay.ForeColor = Color.White;
        this.lblDisplay.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
        this.lblDisplay.TextAlign = ContentAlignment.MiddleRight;
        this.lblDisplay.Text = "0";

        // Button grid layout
        int btnW = 68, btnH = 55, gap = 6;
        int gridLeft = 10, gridTop = 110;

        // Row 0: C, +/-, %, /
        SetupFuncButton(btnClear,      "C",   gridLeft + 0 * (btnW + gap), gridTop + 0 * (btnH + gap), btnW, btnH);
        SetupFuncButton(btnToggleSign, "+/-", gridLeft + 1 * (btnW + gap), gridTop + 0 * (btnH + gap), btnW, btnH);
        SetupFuncButton(btnPercent,    "%",   gridLeft + 2 * (btnW + gap), gridTop + 0 * (btnH + gap), btnW, btnH);
        SetupOpButton(btnDivide,       "÷",   gridLeft + 3 * (btnW + gap), gridTop + 0 * (btnH + gap), btnW, btnH);

        // Row 1: 7, 8, 9, *
        SetupDigitButton(btn7,        "7",   gridLeft + 0 * (btnW + gap), gridTop + 1 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn8,        "8",   gridLeft + 1 * (btnW + gap), gridTop + 1 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn9,        "9",   gridLeft + 2 * (btnW + gap), gridTop + 1 * (btnH + gap), btnW, btnH);
        SetupOpButton(btnMultiply,    "×",   gridLeft + 3 * (btnW + gap), gridTop + 1 * (btnH + gap), btnW, btnH);

        // Row 2: 4, 5, 6, -
        SetupDigitButton(btn4,        "4",   gridLeft + 0 * (btnW + gap), gridTop + 2 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn5,        "5",   gridLeft + 1 * (btnW + gap), gridTop + 2 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn6,        "6",   gridLeft + 2 * (btnW + gap), gridTop + 2 * (btnH + gap), btnW, btnH);
        SetupOpButton(btnSubtract,    "−",   gridLeft + 3 * (btnW + gap), gridTop + 2 * (btnH + gap), btnW, btnH);

        // Row 3: 1, 2, 3, +
        SetupDigitButton(btn1,        "1",   gridLeft + 0 * (btnW + gap), gridTop + 3 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn2,        "2",   gridLeft + 1 * (btnW + gap), gridTop + 3 * (btnH + gap), btnW, btnH);
        SetupDigitButton(btn3,        "3",   gridLeft + 2 * (btnW + gap), gridTop + 3 * (btnH + gap), btnW, btnH);
        SetupOpButton(btnAdd,         "+",   gridLeft + 3 * (btnW + gap), gridTop + 3 * (btnH + gap), btnW, btnH);

        // Row 4: 0 (wide), ., =
        int wideW = btnW * 2 + gap;
        SetupDigitButton(btn0,        "0",   gridLeft + 0 * (btnW + gap), gridTop + 4 * (btnH + gap), wideW, btnH);
        SetupDigitButton(btnDecimal,  ",",   gridLeft + 2 * (btnW + gap), gridTop + 4 * (btnH + gap), btnW, btnH);
        SetupEqualButton(btnEquals,   "=",   gridLeft + 3 * (btnW + gap), gridTop + 4 * (btnH + gap), btnW, btnH);

        // Backspace button (top right of grid)
        SetupFuncButton(btnBackspace, "⌫", gridLeft + 4 * (btnW + gap), gridTop + 0 * (btnH + gap), btnW, btnH);
        btnBackspace.ForeColor = Color.FromArgb(255, 149, 0);

        // History section
        int histLeft = gridLeft + 4 * (btnW + gap);
        this.lblHistoryTitle.Size = new Size(btnW, 24);
        this.lblHistoryTitle.Location = new Point(histLeft, gridTop + 24);
        this.lblHistoryTitle.ForeColor = Color.FromArgb(160, 160, 160);
        this.lblHistoryTitle.Font = new Font("Segoe UI", 9F);
        this.lblHistoryTitle.Text = "Geschiedenis";
        this.lblHistoryTitle.TextAlign = ContentAlignment.MiddleCenter;

        this.listHistory.Size = new Size(btnW + 20, 4 * (btnH + gap));
        this.listHistory.Location = new Point(histLeft - 10, gridTop + 50);
        this.listHistory.BackColor = Color.FromArgb(45, 45, 45);
        this.listHistory.ForeColor = Color.White;
        this.listHistory.BorderStyle = BorderStyle.None;
        this.listHistory.Font = new Font("Segoe UI", 9F);
        this.listHistory.TabStop = false;

        // Add all controls
        this.Controls.Add(lblExpression);
        this.Controls.Add(lblDisplay);
        this.Controls.Add(btn0);
        this.Controls.Add(btn1);
        this.Controls.Add(btn2);
        this.Controls.Add(btn3);
        this.Controls.Add(btn4);
        this.Controls.Add(btn5);
        this.Controls.Add(btn6);
        this.Controls.Add(btn7);
        this.Controls.Add(btn8);
        this.Controls.Add(btn9);
        this.Controls.Add(btnDecimal);
        this.Controls.Add(btnAdd);
        this.Controls.Add(btnSubtract);
        this.Controls.Add(btnMultiply);
        this.Controls.Add(btnDivide);
        this.Controls.Add(btnEquals);
        this.Controls.Add(btnClear);
        this.Controls.Add(btnBackspace);
        this.Controls.Add(btnToggleSign);
        this.Controls.Add(btnPercent);
        this.Controls.Add(lblHistoryTitle);
        this.Controls.Add(listHistory);

        this.ResumeLayout(false);
    }

    private static void SetupDigitButton(Button btn, string text, int x, int y, int w, int h)
    {
        btn.Text = text;
        btn.Location = new Point(x, y);
        btn.Size = new Size(w, h);
        btn.BackColor = Color.FromArgb(60, 60, 60);
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
        btn.Font = new Font("Segoe UI", 14F);
        btn.TabStop = false;
        btn.Cursor = Cursors.Hand;
    }

    private static void SetupOpButton(Button btn, string text, int x, int y, int w, int h)
    {
        SetupDigitButton(btn, text, x, y, w, h);
        btn.BackColor = Color.FromArgb(255, 149, 0);
        btn.ForeColor = Color.White;
        btn.FlatAppearance.BorderColor = Color.FromArgb(200, 120, 0);
    }

    private static void SetupFuncButton(Button btn, string text, int x, int y, int w, int h)
    {
        SetupDigitButton(btn, text, x, y, w, h);
        btn.BackColor = Color.FromArgb(85, 85, 85);
        btn.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
    }

    private static void SetupEqualButton(Button btn, string text, int x, int y, int w, int h)
    {
        SetupOpButton(btn, text, x, y, w, h);
        btn.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
    }

    // Controls
    private Label lblDisplay = null!;
    private Label lblExpression = null!;
    private Label lblHistoryTitle = null!;
    private ListBox listHistory = null!;
    private Button btn0 = null!, btn1 = null!, btn2 = null!, btn3 = null!;
    private Button btn4 = null!, btn5 = null!, btn6 = null!;
    private Button btn7 = null!, btn8 = null!, btn9 = null!;
    private Button btnDecimal = null!;
    private Button btnAdd = null!, btnSubtract = null!, btnMultiply = null!, btnDivide = null!;
    private Button btnEquals = null!;
    private Button btnClear = null!, btnBackspace = null!, btnToggleSign = null!, btnPercent = null!;
}
