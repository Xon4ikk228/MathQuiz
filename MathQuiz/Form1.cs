using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form

    {
        private System.Windows.Forms.Timer timer1;
        // Генератор случайных чисел
        Random randomizer = new Random();

        // Переменные для сложения
        int addend1;
        int addend2;

        // Переменные для вычитания
        int minuend;
        int subtrahend;

        // Переменные для умножения
        int multiplicand;
        int multiplier;

        // Переменные для деления
        int dividend;
        int divisor;

        // Оставшееся время в секундах
        int timeLeft;

        public Form1()
        {
            InitializeComponent();

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;

            sum.ValueChanged += answer_ValueChanged;
            difference.ValueChanged += answer_ValueChanged;
            product.ValueChanged += answer_ValueChanged;
            quotient.ValueChanged += answer_ValueChanged;
        }

        /// <summary>
        /// Запускает тест: генерирует случайные числа и запускает таймер.
        /// </summary>
        public void StartTheQuiz()
        {
            // Сложение
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Вычитание (результат неотрицательный)
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Умножение (от 2 до 10)
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);

            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Деление без остатка
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);

            dividend = divisor * temporaryQuotient;

            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Запуск таймера
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        // Обработчик кнопки Start the quiz
        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        // Обработчик таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();

                MessageBox.Show(
                    "You got all the answers right!",
                    "Congratulations!"
                );

                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";

                MessageBox.Show(
                    "You didn't finish in time.",
                    "Sorry!"
                );

                // Показываем правильные ответы
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                startButton.Enabled = true;
            }
            if (timeLeft <= 5)
            {
                timeLabel.BackColor = Color.Red;
            }
            else
            {
                timeLabel.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Проверяет, все ли ответы правильные.
        /// </summary>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Выделение ответа при получении фокуса
        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        // Звук при правильном ответе
        private void answer_ValueChanged(object sender, EventArgs e)
        {
            if (sender == sum && sum.Value == addend1 + addend2)
                System.Media.SystemSounds.Asterisk.Play();

            else if (sender == difference && difference.Value == minuend - subtrahend)
                System.Media.SystemSounds.Asterisk.Play();

            else if (sender == product && product.Value == multiplicand * multiplier)
                System.Media.SystemSounds.Asterisk.Play();

            else if (sender == quotient && quotient.Value == dividend / divisor)
                System.Media.SystemSounds.Asterisk.Play();
        }
    }
}