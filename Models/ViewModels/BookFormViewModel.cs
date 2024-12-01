using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.ViewModels
{
    public class BookFormViewModel
    {
        // No Create, esse � o livro que ser� criado
        // No Edit, � o livro que est� sendo editado
        public Book Book { get; set; }

        // Essa � a lista de g�neros dispon�veis, buscados no banco de dados
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        // Essa � a lista de g�neros que foram selecionados
        // Usaremos essa propriedade para verificar se foi selecionado algum g�nero
        // E tamb�m para armazenar quais g�neros o usu�rio selecionou
        [Display(Name = "G�neros Liter�rios")]
        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        public List<int> SelectedGenresIds { get; set; } = new List<int>();

        // Embora tenha uma estrutura diferente, isso � uma propriedade tamb�m, mas apenas de leitura
        // Isso porque n�s j� definimos o valor dela de cara
        // O valor dela � o que o m�todo ao lado da arrow function retornar
        public List<SelectListItem> GenresSelect => GenerateGenresSelect();

        // Para exibir um item naquelas listas de sele��o, ele precisa ser um objeto do tipo SelectListItem
        // Aqui pegamos a lista de g�neros, percorremos ela e criamos um objeto desse tipo para cada um
        // Cada um deles tem dois atributos, o Value e o Text
        // O Text � o que aparece na tela, queremos que apare�a o nome do g�nero correspondente
        // O Value � o que � salvo numa lista quando o usu�rio seleciona uma op��o
        // Queremos que sejam salvos os ids dos g�neros que forem selecionados
        private List<SelectListItem> GenerateGenresSelect()
        {
            List<SelectListItem> genresSelect = new List<SelectListItem>();
            if (Genres is not null)
            {
                foreach (Genre genre in Genres)
                {
                    genresSelect.Add(new SelectListItem { Value = genre.Id.ToString(), Text = genre.Name });
                }
            }
            return genresSelect;
        }
    }
}