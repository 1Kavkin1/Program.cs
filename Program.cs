// Підключення бібліотеки Newtonsoft.Json закоментоване,
// тому що у коді використовується System.Text.Json
//using Newtonsoft.Json;

// Підключення бібліотеки для роботи з JSON
using System.Text.Json;

// Простір імен проєкту
namespace FormOptions
{
    // Клас форми реєстрації, який успадковується від Form
    public partial class RegisterForm : Form
    {
        // Змінна для збереження поточної теми (false = світла, true = темна)
        bool isDarkMode = false;

        // Шлях до файлу конфігурації
        string configPath = "appsettings.json";


        // Конструктор форми
        public RegisterForm()
        {
            // Ініціалізація компонентів форми
            InitializeComponent();

            // Подія зміни тексту у полі імені
            // Викликає очищення помилки
            txtName.TextChanged += (s, e) => ClearErrorOnInput(txtName, label8);

            // Подія зміни тексту у полі прізвища
            txtLastName.TextChanged += (s, e) => ClearErrorOnInput(txtLastName, label9);

            // Подія зміни тексту у полі групи
            txtGroup.TextChanged += (s, e) => ClearErrorOnInput(txtGroup, label10);

            // Подія зміни тексту у полі email
            txtEmail.TextChanged += (s, e) => ClearErrorOnInput(txtEmail, label11);

            // Подія зміни тексту у полі пароля
            txtPassword.TextChanged += (s, e) => ClearErrorOnInput(txtPassword, label12);

            // Подія зміни тексту у полі підтвердження пароля
            txtPasswordCheck.TextChanged += (s, e) => ClearErrorOnInput(txtPasswordCheck, label13);
        }

        // Подія завантаження форми
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Завантаження налаштувань
            LoadSettings();

            // Застосування теми
            ApplyTheme();
        }

        // --- Робота з темою та налаштуваннями ---

        // Подія натискання кнопки зміни теми
        private void btnChangeStyles_Click(object sender, EventArgs e)
        {
            // Зміна значення теми на протилежне
            isDarkMode = !isDarkMode;

            // Застосування нової теми
            ApplyTheme();

            // Збереження налаштувань
            SaveSettings();
        }

        // Метод застосування теми
        private void ApplyTheme()
        {
            // Створення локальної змінної для зручності
            bool dark = isDarkMode;

            // Зміна кольору фону форми
            this.BackColor = dark ? Color.FromArgb(26, 26, 26) : SystemColors.Control;

            // Перебір усіх елементів форми
            foreach (Control ctrl in this.Controls)
            {
                // Якщо елемент є Label
                if (ctrl is Label lbl)
                {
                    // Якщо це label помилки
                    if (lbl.Tag?.ToString() == "error")
                    {
                        // Зміна кольору тексту помилки
                        lbl.ForeColor = dark ? Color.LightCoral : Color.Red;
                    }
                    else
                    {
                        // Зміна кольору звичайного тексту
                        lbl.ForeColor = dark ? Color.White : Color.Black;
                    }
                }

                // Якщо елемент є кнопкою
                if (ctrl is Button btn)
                {
                    // Зміна кольору кнопки
                    btn.BackColor = dark ? Color.DimGray : Color.White;

                    // Зміна кольору тексту кнопки
                    btn.ForeColor = dark ? Color.White : Color.Black;
                }
            }

            // Зміна тексту кнопки теми
            btnChangeStyles.Text = dark ? "Світла" : "Темна";
        }

        // Метод завантаження налаштувань
        private void LoadSettings()
        {
            try
            {
                // Перевірка існування файлу конфігурації
                if (File.Exists(configPath))
                {
                    // Зчитування JSON з файлу
                    string jsonString = File.ReadAllText(configPath);

                    // Парсинг JSON документа
                    using (JsonDocument doc = JsonDocument.Parse(jsonString))
                    {
                        // Отримання значення теми
                        isDarkMode = (doc.RootElement.GetProperty("theme").GetString() == "dark");
                    }
                }
            }
            catch
            {
                // Ігнорування помилок
            }
        }

        // Метод збереження налаштувань
        private void SaveSettings()
        {
            try
            {
                // Створення об'єкта з налаштуваннями
                var data = new { theme = isDarkMode ? "dark" : "light" };

                // Серіалізація об'єкта в JSON
                string jsonString = JsonSerializer.Serialize(
                    data,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                // Запис JSON у файл
                File.WriteAllText(configPath, jsonString);
            }
            catch
            {
                // Ігнорування помилок
            }
        }

        // Метод очищення помилки при введенні тексту
        private void ClearErrorOnInput(TextBox textBox, Label errorLabel)
        {
            // Перевірка чи поле не порожнє
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Приховування помилки
                errorLabel.Visible = false;
            }
        }

        // Подія натискання кнопки збереження
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Змінна для перевірки помилок
            bool hasError = false;

            // Перевірка поля імені
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                // Показ помилки
                label8.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка поля прізвища
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                // Показ помилки
                label9.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка поля групи
            if (string.IsNullOrWhiteSpace(txtGroup.Text))
            {
                // Показ помилки
                label10.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // Показ помилки
                label11.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка пароля
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                // Показ помилки
                label12.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка підтвердження пароля
            if (string.IsNullOrWhiteSpace(txtPasswordCheck.Text))
            {
                // Показ помилки
                label13.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Перевірка співпадіння паролів
            if (txtPassword.Text != txtPasswordCheck.Text)
            {
                // Показ помилки
                label13.Visible = true;

                // Встановлення помилки
                hasError = true;
            }

            // Якщо помилок немає
            if (!hasError)
            {
                // Встановлення результату форми
                DialogResult = DialogResult.OK;

                // Створення нового користувача
                User newUser = new User
                {
                    // Запис імені
                    Name = txtName.Text,

                    // Запис прізвища
                    LastName = txtLastName.Text,

                    // Запис групи
                    Group = txtGroup.Text,

                    // Запис email
                    Email = txtEmail.Text,

                    // Запис хешованого пароля
                    Password = hashPasswordMD5(txtPassword.Text)
                };

                // Перетворення об'єкта у JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);

                // Запис JSON у файл
                File.WriteAllText("users.json", json);

                // Створення форми входу
                LoginForm dlg = new LoginForm();

                // Відкриття форми входу
                dlg.ShowDialog();

                // Закриття поточної форми
                this.Close();
            }
        }

        // Подія кнопки показу/приховування пароля
        private void btnVissiblePassword_Click(object sender, EventArgs e)
        {
            // Зміна режиму відображення символів
            txtPassword.UseSystemPasswordChar =
                !txtPassword.UseSystemPasswordChar;
        }

        // Подія кнопки показу/приховування підтвердження пароля
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // Зміна режиму відображення символів
            txtPasswordCheck.UseSystemPasswordChar =
                !txtPasswordCheck.UseSystemPasswordChar;
        }

        // Метод хешування пароля через MD5
        private string hashPasswordMD5(string password)
        {
            // Створення об'єкта MD5
            using var md5 = System.Security.Cryptography.MD5.Create();

            // Перетворення пароля у масив байтів
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);

            // Обчислення хешу
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Повернення хешу у вигляді HEX рядка
            return Convert.ToHexString(hashBytes);
        }
    }
}