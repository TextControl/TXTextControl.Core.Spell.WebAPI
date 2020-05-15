using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TXTextControl.Proofing.Models
{
    public class IncorrectWordModel
    {
        public bool IsDuplicate { get; set; }
        public string Text { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int Index { get; set; }
    }

    public class SynonymGroupModel
    {
        public string PartOfSpeech { get; set; }
        public Synonym[] Synonyms { get; set; }
    }

    public class LanguageScopeModel
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public int Index { get; set; }
        public string Language { get; set; }
    }
}
