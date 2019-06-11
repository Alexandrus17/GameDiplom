using UnityEngine;
using System.Collections.Generic;
using Lean.Common;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization
{
	[CustomEditor(typeof(Localization))]
	public class Localization_Inspector : LeanInspector<Localization>
	{
		static Localization_Inspector()
		{
			AddPresetLanguage("Chinese", "ChineseSimplified", "ChineseTraditional", "zh", "zh-TW", "zh-CN", "zh-HK", "zh-SG", "zh-MO");
			AddPresetLanguage("English", "en", "en-GB", "en-US", "en-AU", "en-CA", "en-NZ", "en-IE", "en-ZA", "en-JM", "en-en029", "en-BZ", "en-BZ", "en-TT", "en-ZW", "en-PH");
			AddPresetLanguage("Spanish", "es", "es-ES", "es-MX", "es-GT", "es-CR", "es-PA", "es-DO", "es-VE", "es-CO", "es-PE", "es-AR", "es-EC", "es-CL", "es-UY", "es-PY", "es-BO", "es-SV", "es-SV", "es-HN", "es-NI", "es-PR");
			AddPresetLanguage("Arabic", "ar", "ar-SA", "ar-IQ", "ar-EG", "ar-LY", "ar-DZ", "ar-MA", "ar-TN", "ar-OM", "ar-YE", "ar-SY", "ar-JO", "ar-LB", "ar-KW", "ar-AE", "ar-BH", "ar-QA");
			AddPresetLanguage("German", "de", "de-DE", "de-CH", "de-AT", "de-LU", "de-LI");
			AddPresetLanguage("Korean", "ko", "ko-KR");
			AddPresetLanguage("French", "fr", "fr-FR", "fr-BE", "fr-CA", "fr-CH", "fr-LU", "fr-MC");
			AddPresetLanguage("Russian", "ru", "ru-RU");
			AddPresetLanguage("Japanese", "ja", "ja-JP");
			AddPresetLanguage("Italian", "it", "it-IT", "it-CH");
			AddPresetLanguage("Portuguese", "pt", "pt-BR", "pt-PT");
			AddPresetLanguage("Other...");
		}

		protected override void DrawInspector()
		{
			Localization.UpdateTranslations();

			Draw("DefaultLanguage", "If the application is started and no language has been loaded or auto detected, this language will be used.");
			Draw("DetectLanguage", "How should the cultures be used to detect the user's device language?");
			Draw("SaveLanguage", "Automatically save/load the CurrentLanguage selection to PlayerPrefs? (can be cleared with ClearSave context menu option)");

			EditorGUILayout.Separator();

			DrawCurrentLanguage();

			EditorGUILayout.Separator();

			DrawLanguages();

			EditorGUILayout.Separator();

			DrawPhrases();
		}

		private void DrawCurrentLanguage()
		{
			var rect = Reserve();
			var rectA = rect; rectA.xMax -= 37.0f;
			var rectB = rect; rectB.xMin = rectB.xMax - 35.0f;

			Localization.CurrentLanguage = EditorGUI.TextField(rectA, "Current Language", Localization.CurrentLanguage);

			if (GUI.Button(rectB, "List") == true)
			{
				var menu = new GenericMenu();

				foreach (var pair in Localization.CurrentLanguages)
				{
					var languageName = pair.Key;

					menu.AddItem(new GUIContent(languageName), Localization.CurrentLanguage == languageName, () => { Localization.CurrentLanguage = languageName; });
				}

				if (menu.GetItemCount() > 0)
				{
					menu.DropDown(rectB);
				}
				else
				{
					Debug.LogWarning("Your scene doesn't contain any languages, so the language name list couldn't be created.");
				}
			}
		}

		private void DrawLanguages()
		{
			var languagesProperty = serializedObject.FindProperty("languages");

			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Languages", EditorStyles.boldLabel);
				if (GUILayout.Button("Add", EditorStyles.miniButton, GUILayout.Width(35.0f)) == true)
				{
					var menu = new GenericMenu();

					foreach (var presetLanguage in presetLanguages)
					{
						var preset = presetLanguage; menu.AddItem(new GUIContent(presetLanguage.Name), Target.LanguageExists(presetLanguage.Name), () => AddLanguage(preset));
					}

					menu.ShowAsContext();
				}
			EditorGUILayout.EndHorizontal();

			if (languagesProperty.arraySize == 0)
			{
				EditorGUILayout.HelpBox("Click the 'Add' button, and select a language.", MessageType.Info);
			}

			EditorGUI.indentLevel++;
				for (var i = 0; i < languagesProperty.arraySize; i++)
				{
					EditorGUILayout.PropertyField(languagesProperty.GetArrayElementAtIndex(i), true);
				}
			EditorGUI.indentLevel--;
		}

		private void DrawPhrases()
		{
			var rectA = Reserve();
			var rectB = rectA; rectB.xMin += EditorGUIUtility.labelWidth; rectB.xMax -= 37.0f;
			var rectC = rectA; rectC.xMin = rectC.xMax - 35.0f;
			EditorGUI.LabelField(rectA, "Phrases", EditorStyles.boldLabel);
			addPhraseName = EditorGUI.TextField(rectB, "", addPhraseName);
			EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(addPhraseName) == true || Localization.CurrentPhrases.ContainsKey(addPhraseName) == true);
				if (GUI.Button(rectC, "Add", EditorStyles.miniButton) == true)
				{
					var phrase = Localization.AddPhraseToFirst(addPhraseName);

					Localization.UpdateTranslations();

					EditorGUIUtility.PingObject(phrase);
				}
			EditorGUI.EndDisabledGroup();

			if (Localization.CurrentPhrases.Count == 0)
			{
				EditorGUILayout.HelpBox("Type in the name of a phrase, and click the 'Add' button.", MessageType.Info);
			}
			else
			{
				var total = 0;

				EditorGUI.indentLevel++;
					EditorGUI.BeginDisabledGroup(true);
						foreach (var pair in Localization.CurrentPhrases)
						{
							if (string.IsNullOrEmpty(addPhraseName) == true || pair.Key.IndexOf(addPhraseName, System.StringComparison.InvariantCultureIgnoreCase) >= 0)
							{
								EditorGUILayout.ObjectField(pair.Key, pair.Value, typeof(Object), true); total++;
							}
						}
					EditorGUI.EndDisabledGroup();
				EditorGUI.indentLevel--;

				if (total == 0)
				{
					EditorGUILayout.HelpBox("No phrase with this title exists, click the 'Add' button to create it.", MessageType.Info);
				}
			}
		}

		private void AddLanguage(PresetLanguage presetLanguage)
		{
			Undo.RecordObject(Target, "Add Language");

			Target.AddLanguage(presetLanguage.Name, presetLanguage.Cultures);

			Dirty();
		}

		private static void AddPresetLanguage(string name, params string[] cultures)
		{
			var presetLanguage = new PresetLanguage();

			presetLanguage.Name     = name;
			presetLanguage.Cultures = cultures;

			presetLanguages.Add(presetLanguage);
		}

		[MenuItem("GameObject/Lean/Localization", false, 1)]
		private static void CreateLocalization()
		{
			var gameObject = new GameObject(typeof(Localization).Name);

			Undo.RegisterCreatedObjectUndo(gameObject, "Create Localization");

			gameObject.AddComponent<Localization>();

			Selection.activeGameObject = gameObject;
		}

		class PresetLanguage
		{
			public string   Name;
			public string[] Cultures;
		}

		private static string addPhraseName = "";

		private static List<PresetLanguage> presetLanguages = new List<PresetLanguage>();
	}
}
#endif

namespace Lean.Localization
{
	/
	[ExecuteInEditMode]
	[HelpURL(HelpUrlPrefix + "Localization")]
	[AddComponentMenu(ComponentPathPrefix + "Localization")]
	public class Localization : MonoBehaviour
	{
		public enum DetectType
		{
			None,
			SystemLanguage,
			CurrentCulture,
			CurrentUICulture
		}

		public const string HelpUrlPrefix = LeanHelper.HelpUrlPrefix + "Localization#";

		public const string ComponentPathPrefix = LeanHelper.ComponentPathPrefix + "Localization/Lean ";

		/
		public static List<Localization> Instances = new List<Localization>();

		public static Dictionary<string, LeanPhrase> CurrentPhrases = new Dictionary<string, LeanPhrase>();

		public static Dictionary<string, LeanLanguage> CurrentLanguages = new Dictionary<string, LeanLanguage>();

		/
		public static Dictionary<string, LeanTranslation> CurrentTranslations = new Dictionary<string, LeanTranslation>();

		/
		[LeanLanguageName]
		public string DefaultLanguage;

		/
		public DetectType DetectLanguage = DetectType.SystemLanguage;

		/
		public bool SaveLanguage = true;

		[SerializeField]
		private List<LeanLanguage> languages;

		/
		public static System.Action OnLocalizationChanged;

		/
		private static string currentLanguage;

		private static bool pendingUpdates;

		public List<LeanLanguage> Languages
		{
			get
			{
				if (languages == null)
				{
					languages = new List<LeanLanguage>();
				}

				return languages;
			}
		}

		/
		public static bool CurrentSaveLanguage
		{
			get
			{
				for (var i = 0; i < Instances.Count; i++)
				{
					if (Instances[i].SaveLanguage == true)
					{
						return true;
					}
				}

				return false;
			}
		}

		/
		public static string CurrentLanguage
		{
			set
			{
				if (CurrentLanguage != value)
				{
					currentLanguage = value;

					UpdateTranslations();

					if (CurrentSaveLanguage == true)
					{
						PlayerPrefs.SetString("Localization.CurrentLanguage", value);
					}
				}
			}

			get
			{
				return currentLanguage;
			}
		}

		[ContextMenu("Clear Save")]
		public void ClearSave()
		{
			PlayerPrefs.DeleteKey("Localization.CurrentLanguage");
		}

		/
		public void SetCurrentLanguage(string newLanguage)
		{
			CurrentLanguage = newLanguage;
		}

		/
		public void SetCurrentLanguage(int newLanguageIndex)
		{
			if (newLanguageIndex >= 0 && newLanguageIndex < Instances.Count)
			{
				SetCurrentLanguage(Instances[newLanguageIndex].name);
			}
		}

		public bool LanguageExists(string languageName)
		{
			var language = default(LeanLanguage);

			return TryGetLanguage(languageName, ref language);
		}

		public bool TryGetLanguage(string languageName, ref LeanLanguage language)
		{
			if (languages != null)
			{
				for (var i = languages.Count - 1; i >= 0; i--)
				{
					language = languages[i];

					if (language.Name == languageName)
					{
						return true;
					}
				}
			}

			return false;
		}

		public LeanLanguage AddLanguage(string languageName, string[] cultures)
		{
			var language = default(LeanLanguage);

			if (TryGetLanguage(languageName, ref language) == false)
			{
				language = new LeanLanguage();

				language.Name = languageName;

				if (languages == null)
				{
					languages = new List<LeanLanguage>();
				}

				languages.Add(language);
			}

			language.Cultures.Clear();
			language.Cultures.AddRange(cultures);

			return language;
		}

		public static LeanPhrase AddPhraseToFirst(string title)
		{
			if (Instances.Count == 0)
			{
				new GameObject("Localization").AddComponent<Localization>();
			}

			return Instances[0].AddPhrase(title);
		}

		public LeanPhrase AddPhrase(string title)
		{
			if (string.IsNullOrEmpty(title) == false)
			{
				var root   = transform;
				var tokens = title.Split('/');

				for (var i = 0; i < tokens.Length; i++)
				{
					var token = tokens[i];
					var next  = root.Find(token);

					if (next == null)
					{
						next = new GameObject(token).transform;
						next.SetParent(root, false);
					}

					root = next;
				}

				var phrase = root.GetComponent<LeanPhrase>();

				if (phrase == null)
				{
					phrase = root.gameObject.AddComponent<LeanPhrase>();
				}

				return phrase;
			}

			return null;
		}

		public static LeanTranslation AddTranslationToFirst(string title, string language, string text, Object obj = null)
		{
			var phrase = AddPhraseToFirst(title);

			if (phrase != null)
			{
				return phrase.AddTranslation(language, text, obj);
			}

			return null;
		}

		public LeanTranslation AddTranslation(string title, string language, string text, Object obj = null)
		{
			var phrase = AddPhrase(title);

			if (phrase != null)
			{
				return phrase.AddTranslation(language, text, obj);
			}

			return null;
		}

		/
		public static bool PhraseExists(string title)
		{
			var phrase = default(LeanPhrase);

			return CurrentPhrases.TryGetValue(title, out phrase);
		}

		/
		public static LeanTranslation GetTranslation(string path)
		{
			var translation = default(LeanTranslation);

			TryGetTranslation(path, ref translation);

			return translation;
		}

		public static bool TryGetTranslation(string path, ref LeanTranslation translation)
		{
			if (path != null)
			{
				if (CurrentTranslations.TryGetValue(path, out translation) == true)
				{
					return true;
				}
			}

			return false;
		}

		/
		public static string GetTranslationText(string path, string fallbackText = null)
		{
			var translation = default(LeanTranslation);

			if (TryGetTranslation(path, ref translation) == true)
			{
				return translation.Text;
			}

			return fallbackText;
		}

		/
		public static Object GetTranslationObject(string path, Object fallbackObject = null)
		{
			var translation = default(LeanTranslation);

			if (TryGetTranslation(path, ref translation) == true)
			{
				return translation.Object;
			}

			return fallbackObject;
		}

		/
		public static void UpdateTranslations()
		{
			pendingUpdates = false;

			CurrentPhrases.Clear();
			CurrentLanguages.Clear();
			CurrentTranslations.Clear();

			
			for (var i = 0; i < Instances.Count; i++)
			{
				Instances[i].Register();
			}

			
			if (OnLocalizationChanged != null)
			{
				OnLocalizationChanged();
			}
		}

		public static void DelayUpdateTranslations()
		{
			pendingUpdates = true;

#if UNITY_EDITOR
			
			for (var i = 0; i < Instances.Count; i++)
			{
				EditorUtility.SetDirty(Instances[i].gameObject);
			}
#endif
		}

		private void Register()
		{
			if (languages != null)
			{
				for (var i = 0; i < languages.Count; i++)
				{
					var language = languages[i];

					if (CurrentLanguages.ContainsKey(language.Name) == false)
					{
						CurrentLanguages.Add(language.Name, language);
					}
				}
			}

			for (var i = 0; i < transform.childCount; i++)
			{
				Register(transform.GetChild(i), "");
			}
		}

		private void Register(Transform root, string path)
		{
			var phrase = root.GetComponent<LeanPhrase>();

			if (phrase != null && phrase.isActiveAndEnabled == true)
			{
				var finalPath = path + phrase.name;

				if (CurrentPhrases.Remove(finalPath) == true)
				{
					
				}

				CurrentPhrases.Add(finalPath, phrase);

				phrase.Register(finalPath, DefaultLanguage);
			}

			for (var i = 0; i < root.childCount; i++)
			{
				Register(root.GetChild(i), path + root.name + "/");
			}
		}

		/
		protected virtual void OnEnable()
		{
			Instances.Add(this);

			UpdateCurrentLanguage();

			UpdateTranslations();
		}

		/
		protected virtual void OnDisable()
		{
			Instances.Remove(this);

			UpdateTranslations();
		}

		protected virtual void Update()
		{
			if (pendingUpdates == true)
			{
				UpdateTranslations();
			}
		}
#if UNITY_EDITOR
		
		protected virtual void OnValidate()
		{
			UpdateTranslations();
		}
#endif
		private void UpdateCurrentLanguage()
		{
			
			if (string.IsNullOrEmpty(currentLanguage) == true)
			{
				if (SaveLanguage == true)
				{
					currentLanguage = PlayerPrefs.GetString("Localization.CurrentLanguage");
				}
			}

			
			if (string.IsNullOrEmpty(currentLanguage) == true)
			{
				switch (DetectLanguage)
				{
					case DetectType.SystemLanguage:
					{
						currentLanguage = FindLanguageName(Application.systemLanguage.ToString());
					}
					break;

					case DetectType.CurrentCulture:
					{
						var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;

						if (cultureInfo != null)
						{
							currentLanguage = FindLanguageName(cultureInfo.Name);
						}
					}
					break;

					case DetectType.CurrentUICulture:
					{
						var cultureInfo = System.Globalization.CultureInfo.CurrentUICulture;

						if (cultureInfo != null)
						{
							currentLanguage = FindLanguageName(cultureInfo.Name);
						}
					}
					break;
				}
			}

			
			if (string.IsNullOrEmpty(currentLanguage) == true)
			{
				currentLanguage = DefaultLanguage;
			}
		}

		private string FindLanguageName(string alias)
		{
			for (var i = Languages.Count - 1; i >= 0; i--)
			{
				var language = Languages[i];

				if (language.Name == alias)
				{
					return language.Name;
				}

				if (language.Cultures != null)
				{
					for (var j = language.Cultures.Count - 1; j >= 0; j--)
					{
						if (language.Cultures[j] == alias)
						{
							return language.Name;
						}
					}
				}
			}

			return null;
		}
	}
}