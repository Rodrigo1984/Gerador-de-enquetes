using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_enquetes
{
    /// <summary>
    /// Opção de uma enquete.
    /// </summary>
    internal class Option 
    {
        /// <summary>
        /// ID da opção (p que deve ser digitado para escolher a opção).
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Texto associado á opção.
        /// </summary>
        public string Text { get; set; }

        /// <see cref="SurveyIO.Save()"/>
        public void Save(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Text);
        }

        /// <see cref="SurveyIO.Load()"/>
        public void Load(BinaryReader reader)
        {
            Id = reader.ReadString();
            Text = reader.ReadString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Option);
        }

        public bool Equals(Option other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
