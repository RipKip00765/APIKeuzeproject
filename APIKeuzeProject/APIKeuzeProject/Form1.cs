using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace APIKeuzeProject
{
    public partial class Form1 : Form
    {
        private List<TriviaQuestion> triviaQuestions;
        private int currentQuestionIndex = 0;
        public int AantalCorrect = 0;
        public int AantalFout = 0;
        public Form1()
        {
            InitializeComponent();
            LoadTriviaQuestions();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void LoadTriviaQuestions()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("https://opentdb.com/api.php?amount=20&category=9&difficulty=medium&type=multiple");
                    var triviaData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                    var results = JsonConvert.DeserializeObject<List<TriviaQuestion>>(triviaData["results"].ToString());
                    triviaQuestions = results;

                    DisplayQuestion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het laden van de vragen: " + ex.Message);
            }
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < triviaQuestions.Count)
            {
                var question = triviaQuestions[currentQuestionIndex];
                lblQuestion.Text = question.question;

                List<string> allAnswers = new List<string>(question.incorrect_answers)
            {
                question.correct_answer
            };
                Random rand = new Random();
                allAnswers = allAnswers.OrderBy(x => rand.Next()).ToList();

                btnAnswer1.Text = allAnswers[0];
                btnAnswer2.Text = allAnswers[1];
                btnAnswer3.Text = allAnswers[2];
                btnAnswer4.Text = allAnswers[3];
            }
        }

        private void CheckAnswer(string selectedAnswer)
        {
            var question = triviaQuestions[currentQuestionIndex];

            if (selectedAnswer == question.correct_answer)
            {
                MessageBox.Show("Correct!");
                AantalCorrect++;
                CorrectLBL.Text = AantalCorrect.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect. Het juiste antwoord was: " + question.correct_answer);
                AantalFout++;
                IncorrectLBL.Text = AantalFout.ToString();
            }

            currentQuestionIndex++;

            if (currentQuestionIndex < triviaQuestions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                MessageBox.Show("Je hebt alle vragen beantwoord!");
                Eindscherm();
            }
        }

        private void btnAnswer1_Click(object sender, EventArgs e)
        {
            CheckAnswer(btnAnswer1.Text);
        }

        private void btnAnswer2_Click(object sender, EventArgs e)
        {
            CheckAnswer(btnAnswer2.Text);
        }

        private void btnAnswer3_Click(object sender, EventArgs e)
        {
            CheckAnswer(btnAnswer3.Text);
        }

        private void btnAnswer4_Click(object sender, EventArgs e)
        {
            CheckAnswer(btnAnswer4.Text);
        }

        public class TriviaQuestion
        {
            public string question { get; set; }
            public string correct_answer { get; set; }
            public List<string> incorrect_answers { get; set; }
        }

        public void Eindscherm()
        {

        }

    }
}
