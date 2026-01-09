## Fix Language Localization Issues

### Issues Identified
1. **Initialization Setup Interface Always Shows Simplified Chinese**
   - WelcomeWindow.xaml has hardcoded Chinese text
   - SelectDatabasePath_OnClick method has hardcoded Chinese

2. **Settings Interface Multi-language Function Invalid**
   - WelcomeModel uses "en" instead of "en-US" (mismatch with languages.json)
   - Culture code format mismatch between WelcomeWindow and languages.json

### Fix Steps
1. **Fix Hardcoded Chinese in WelcomeWindow.xaml**
   - Replace line 45: "设置数据库位置和语言" with `{lex:Loc Welcome.DatabaseLanguage.Title}`
   - Replace line 46: "请选择数据库文件的保存位置和界面语言设置。" with `{lex:Loc Welcome.DatabaseLanguage.Subtitle}`

2. **Fix Hardcoded Chinese in WelcomeWindow.xaml.cs**
   - Replace line 265: "选择数据库保存位置" with `GetLocalizedText("Welcome.DatabaseLanguage.SelectLocation")`

3. **Fix Culture Code Mismatch**
   - Update WelcomeModel.SupportedLanguages to use "en-US" instead of "en"
   - Update WelcomeModel.SelectedLanguage logic to use "en-US" instead of "en"
   - Ensure language codes match between WelcomeWindow, languages.json, and Settings class

4. **Verify Localization Keys**
   - Ensure all localization keys exist in the language files
   - Test the fix by running the application

### Expected Results
- Initialization setup interface respects system language
- Settings interface multi-language function works correctly
- All text is properly localized based on user selection