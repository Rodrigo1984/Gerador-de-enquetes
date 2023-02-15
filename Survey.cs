using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_enquetes
{
    internal class Survey : IStorable
    {
        /// <summary>
        /// diretório que mapeia um ID de op~ção para uma opção.
        /// </summary>
        private IDictionary<string, Option> _options = new Dictionary<string, Option>();

        /// <summary>
        /// Referencia objeto responsável por calcular os votos.
        /// </summary>
        private Votes votes;

        /// <summary>
        /// Questão da enquete.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Contador de votos da enquete.
        /// </summary>
        public int VoteCount
        {
            get
            {
                // Delega a chamada para VoteCount do objeto Votes.
                return votes.VotesCount;
            }
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        public Survey()
        {
            // Instancia o objeto que calcula os votos.
            votes = new Votes(this);
        }

        /// <summary>
        /// Adiciona ou altera uma opção da enquete. Se o ID ainda não exite, adiciona, senão altera.
        /// </summary>
        /// <param name="id">ID da opção.</param>
        /// <param name="text">Texto da opção.</param>
        public void SetOption(string id, string text)
        {
            // Cria a opção, convertendo o ID para maiúsculo.
            Option option = new Option();
            option.Id = id.ToUpper();
            option.Text = text;

            if (!_options.ContainsKey(id))
            {
                // Adiciona se o ID não existe.
                _options.Add(id, option);
            }
            else
            {
                // Altera se o ID já existe.
                _options[id] = option;
            }
        }

        /// <summary>
        /// Retorna a enquete em um formato de string.
        /// </summary>
        /// <returns></returns>
        public string GetFormattedSurvey()
        {
            // Usa um StringBuilder para evitar concatenações de strings.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Question);

            foreach (Option option in _options.Values)
            {
                sb.Append(option.Id).Append(" - ").AppendLine(option.Text);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Vota na enquete, através da digitação da opção no console.
        /// </summary>
        /// <param name="option">Receberá o objeto Option assiado ao voto.</param>
        /// <param name="vote">Receberá o voto dado pelo usuário.</param>
        /// <returns>true de o voto foi em uma opção válida; false, caso contrario.</returns>
        public bool Vote(out Option option, out string vote)
        {
            // Lê o voto.
            vote = Console.ReadLine();

            // Converte o voto para maiúsculo.
            vote = vote.ToUpper();

            // Busca o objeto no dicionario.
            bool valid = _options.TryGetValue(vote, out option);

            if (valid)
            {
                // Caso tenha encontrado, computa o voto.
                votes.AddVote(option);
            }

            return valid;
        }

        /// <summary>
        /// Calcula os votos da enquete.
        /// </summary>
        /// <param name="sort">Indica se o resultado deve ser ordenado em ordem decrescente de número de votos (o padrão é true).</param>
        /// <returns>Lista de objetos OptionScore representando os votos.</returns>
        public List<OptionScore> CalculateScores(bool sort = true)
        {
            // Delega o cálculo para o objeto Votes.
            return votes.CalculateScore(sort);
        }

        /// <see cref="SurveyIO.Save()"/>
        public void Save(BinaryWriter writer)
        {
            // Salva a questão, o número de opções e depois cada uma das opções.
            writer.Write(Question);
            writer.Write(_options.Count);

            foreach (Option option in _options.Values)
            {
                // Chama o Save() de Option para salvar a opção.
                option.Save(writer);
            }

            // Salva os votos da enquete.
            votes.Save(writer);
        }

        public void Load(BinaryReader reader)
        {
            Question = reader.ReadString();

            _options = new Directory<string, Option>();
            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                Option option = new Option();

                _options[option.Id] = option;
            }

            votes.Load(reader);

        }

    }
}
