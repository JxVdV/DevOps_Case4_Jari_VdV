using Dev_OpsWPF.DAL;
using Dev_OpsWPF.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dev_OpsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CharacterRepository characterRepository;
        private SortedSet<Character> characters;

        public MainWindow()
        {
            InitializeComponent();

            characterRepository = new CharacterRepository();
            characters = new SortedSet<Character>(characterRepository.GetCharacters());
            foreach (Character character in characters)
            {
                string name = character.Name;
                lstCharacters.Items.Add(name);
            }
        }

        private void ButtonAddCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCharacter.Text) && !lstCharacters.Items.Contains(txtCharacter.Text))
            {
                string name = txtCharacter.Text;
                lstCharacters.Items.Add(name);
                Character character = new Character { Name = name };
                characterRepository.InsertCharacter(character);
                txtCharacter.Clear();
            }
        }

        private void ButtonDeleteCharacter_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = lstCharacters.SelectedItem;
            string name = lstCharacters.SelectedValue.ToString();
            characterRepository.DeleteCharacter(name);
            Console.WriteLine(name);
            lstCharacters.Items.Remove(selectedItem);
        }

        private void SelectApi(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectList(object sender, SelectionChangedEventArgs e)
        {

        }

        private static readonly HttpClient client = new HttpClient();

        public async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            {
                lstApis.Items.Add(repo.Name);
            }
        }


        private async Task<List<Api>> ProcessRepositories()
        {
            // special url to only get first 100 characters -> reduced
            var streamTask = client.GetStreamAsync("https://rickandmortyapi.com/api/character/[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100]");
            var repositories = await JsonSerializer.DeserializeAsync<List<Api>>(await streamTask);

            return repositories;
        }
    }
}