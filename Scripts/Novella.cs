using UnityEngine;

    public class Novella : Command, ILocalizable
    {
        [TextArea(5,10)]
        [SerializeField] protected string storyText = "";

        [Tooltip("Notes about this story text for other authors, localization, etc.")]
        [SerializeField] protected string description = "";

        [Tooltip("Character that is speaking")]
        [SerializeField] protected Character character;

        [Tooltip("Portrait that represents speaking character")]
        [SerializeField] protected Sprite portrait;

        [Tooltip("Voiceover audio to play when writing the text")]
        [SerializeField] protected AudioClip voiceOverClip;

        [Tooltip("Always show this Novella text when the command is executed multiple times")]
        [SerializeField] protected bool showAlways = true;

        [Tooltip("Number of times to show this Novella text when the command is executed multiple times")]
        [SerializeField] protected int showCount = 1;

        [Tooltip("Type this text in the previous dialog box.")]
        [SerializeField] protected bool extendPrevious = false;

        [Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
        [SerializeField] protected bool fadeWhenDone = true;

        [Tooltip("Wait for player to click before continuing.")]
        [SerializeField] protected bool waitForClick = true;

        [Tooltip("Stop playing voiceover when text finishes writing.")]
        [SerializeField] protected bool stopVoiceover = true;

        [Tooltip("Wait for the Voice Over to complete before continuing")]
        [SerializeField] protected bool waitForVO = false;

        [Tooltip("Sets the active Novella dialog with a reference to a Novella Dialog object in the scene. All story text will now display using this Novella Dialog.")]
        [SerializeField] protected NovellaDialog setNovellaDialog;

        protected int executionCount;

        #region Public members
		
        public virtual Character _Character { get { return character; } }

        public virtual Sprite Portrait { get { return portrait; } set { portrait = value; } }

        public virtual bool ExtendPrevious { get { return extendPrevious; } }

        public override void OnEnter()
        {
            if (!showAlways && executionCount >= showCount)
            {
                Continue();
                return;
            }

            executionCount++;

            if (character != null && character.SetNovellaDialog != null)
            {
                NovellaDialog.ActiveNovellaDialog = character.SetNovellaDialog;
            }

            if (setNovellaDialog != null)
            {
                NovellaDialog.ActiveNovellaDialog = setNovellaDialog;
            }

            var NovellaDialog = NovellaDialog.GetNovellaDialog();
            if (NovellaDialog == null)
            {
                Continue();
                return;
            }
    
            var flowchart = GetFlowchart();

            NovellaDialog.SetActive(true);

            NovellaDialog.SetCharacter(character);
            NovellaDialog.SetCharacterImage(portrait);

            string displayText = storyText;

            var activeCustomTags = CustomTag.activeCustomTags;
            for (int i = 0; i < activeCustomTags.Count; i++)
            {
                var ct = activeCustomTags[i];
                displayText = displayText.Replace(ct.TagStartSymbol, ct.ReplaceTagStartWith);
                if (ct.TagEndSymbol != "" && ct.ReplaceTagEndWith != "")
                {
                    displayText = displayText.Replace(ct.TagEndSymbol, ct.ReplaceTagEndWith);
                }
            }

            string subbedText = flowchart.SubstituteVariables(displayText);

            NovellaDialog.Novella(subbedText, !extendPrevious, waitForClick, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, delegate {
                Continue();
            });
        }

        public override string GetSummary()
        {
            string namePrefix = "";
            if (character != null) 
            {
                namePrefix = character.NameText + ": ";
            }
            if (extendPrevious)
            {
                namePrefix = "EXTEND" + ": ";
            }
            return namePrefix + "\"" + storyText + "\"";
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        public override void OnReset()
        {
            executionCount = 0;
        }

        public override void OnStopExecuting()
        {
            var NovellaDialog = NovellaDialog.GetNovellaDialog();
            if (NovellaDialog == null)
            {
                return;
            }

            NovellaDialog.Stop();
        }

        #endregion

        #region ILocalizable implementation

        public virtual string GetStandardText()
        {
            return storyText;
        }

        public virtual void SetStandardText(string standardText)
        {
            storyText = standardText;
        }

        public virtual string GetDescription()
        {
            return description;
        }
        
        public virtual string GetStringId()
        {
            string stringId = "Novella." + GetFlowchartLocalizationId() + "." + itemId + ".";
            if (character != null)
            {
                stringId += character.NameText;
            }

            return stringId;
        }

        #endregion
    }