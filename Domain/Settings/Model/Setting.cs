
using System.Collections.Generic;

namespace Domain.Settings.Model
{
    public class GroupSettings
    {
        private IList<Keyword> Keywords
        {
            get => default;
            set
            {
            }
        }

        private IList<Acronym> Acronyms
        {
            get => default;
            set
            {
            }
        }

        private IList<DictionaryContext> Dictionaries
        {
            get => default;
            set
            {
            }
        }

        private IList<TaskSetting> TaskSettings
        {
            get => default;
            set
            {
            }
        }
    }
}