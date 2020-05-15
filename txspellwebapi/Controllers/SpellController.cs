using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TXTextControl.Proofing;
using TXTextControl.Proofing.Models;

namespace txspellwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellController : ControllerBase
    {
        [HttpGet]
        [Route("Check")]
        public List<IncorrectWordModel> Check(string text, string language = "en_US")
        {
            if(text == null)
                return null;

            // create a new spell checking engine
            TXTextControl.Proofing.TXSpell spell = new TXTextControl.Proofing.TXSpell();
            spell.Create();

            TXTextControl.Proofing.OpenOfficeDictionary dict = new OpenOfficeDictionary(@"Dictionaries\en_US.dic");
            spell.Dictionaries.Add(dict);

            spell.Check(text, new System.Globalization.CultureInfo(language));

            List<IncorrectWordModel> lIncorrectWords = new List<IncorrectWordModel>();

            var incorrectWords = spell.IncorrectWords;

            if (incorrectWords == null)
                return null;

            foreach (IncorrectWord word in incorrectWords)
            {
                lIncorrectWords.Add(new IncorrectWordModel()
                {
                    Text = word.Text,
                    Index = word.Index,
                    IsDuplicate = word.IsDuplicate,
                    Length = word.Length,
                    Start = word.Start
                });
            }

            return lIncorrectWords;
        }

        [HttpGet]
        [Route("CreateSuggestions")]
        public List<string> CreateSuggestions(string incorrectWord, string language = "en_US", int max = 10)
        {
            if (incorrectWord == null)
                return null;

            // create a new spell checking engine
            TXTextControl.Proofing.TXSpell spell = new TXTextControl.Proofing.TXSpell();
            spell.Create();

            TXTextControl.Proofing.OpenOfficeDictionary dict = new OpenOfficeDictionary(@"Dictionaries\en_US.dic");
            spell.Dictionaries.Add(dict);

            spell.CreateSuggestions(incorrectWord, max, new CultureInfo(language));
  
            List<string> lSuggestions = new List<string>();

            var suggestions = spell.Suggestions;

            if (suggestions == null)
                return null;

            foreach (Suggestion suggestion in suggestions)
            {
                lSuggestions.Add(suggestion.Text);
            }
            
            return lSuggestions;
        }

        [HttpGet]
        [Route("CreateSynonyms")]
        public List<SynonymGroupModel> CreateSynonyms(string text, string language = "en_US")
        {
            if (text == null)
                return null;

            // create a new spell checking engine
            TXTextControl.Proofing.TXSpell spell = new TXTextControl.Proofing.TXSpell();
            spell.Create();

            TXTextControl.Proofing.SynonymList synonymList = new SynonymList(@"SynonymLists\th_en_US_v2.dat");
            spell.SynonymLists.Add(synonymList);

            List<SynonymGroupModel> lSynonymGroups = new List<SynonymGroupModel>();

            var synonymGroups = spell.CreateSynonyms(text, new CultureInfo(language));

            if (synonymGroups == null)
                return null;

            foreach (SynonymGroup group in synonymGroups)
            {
                lSynonymGroups.Add(new SynonymGroupModel()
                {
                    PartOfSpeech = group.PartOfSpeech,
                    Synonyms = group.Synonyms
                });
            }

            return lSynonymGroups;
        }

        [HttpGet]
        [Route("DetectLanguageScopes")]
        public List<LanguageScopeModel> DetectLanguageScopes(string text)
        {
            if (text == null)
                return null;

            // create a new spell checking engine
            TXTextControl.Proofing.TXSpell spell = new TXTextControl.Proofing.TXSpell();
            spell.Create();

            spell.DetectLanguageScopes(text);

            List<LanguageScopeModel> lLanguageScopes = new List<LanguageScopeModel>();

            var languageScopes = spell.LanguageScopes;

            if (languageScopes == null)
                return null;

            foreach (LanguageScope scope in languageScopes)
            {
                if (scope.Language == null)
                    continue;

                lLanguageScopes.Add(new LanguageScopeModel()
                {
                    Index = scope.Index,
                    Language = scope.Language.Name,
                    Length = scope.Length,
                    Start = scope.Start
                });
            }

            return lLanguageScopes;
        }
    }
}