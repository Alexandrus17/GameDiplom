using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Berry.Utils;
// Класс, отвечающий логику игры 
public class MatchThree : MonoBehaviour 
{
   
    public static MatchThree main;
    public bool squareCombination = true;
    public List<Combinations> combinations = new List<Combinations>();
    public List<ChipInfo> chipInfos = new List<ChipInfo>();
    public List<BlockInfo> blockInfos = new List<BlockInfo>();
    public List<Mix> mixes = new List<Mix>();
    List<Solution> solutions = new List<Solution>();
    public int lastMovementId;
    public int movesCount; 
    public int swapEvent; 
    public int eventCount; 
    public int score = 0; 
    public int[] colorMask = new int[6]; 
    public bool isPlaying = false;
    public bool outOfLimit = false;
    public bool reachedTheTarget = false;
    public int creatingSugarTask = 0;
    public bool firstChipGeneration = false;
    public int matchCount = 0;
    public int stars;
    // Добавленые параметры
    public int eat;
    public int energy;
    public int money;
    public int coffee;
    public int hygiene;
    //Проверка исправности
    public bool coffeevarka;
    public bool multvarka;
    public bool icebox;
    public bool shower;
    public bool bed;
    public bool computer;
    //время
    public int TimeMinute;
    public int TimeHour;
    public int LifeDays;
    bool targetRoutineIsOver = false;
    bool limitationRoutineIsOver = false;
    public static int scoreC = 10; 
    void Awake() // стандартная функция Unity, запускается 1 раз в самом начале
	{  
        main = this;
        combinations.Sort((Combinations a, Combinations b) => {
            if (a.priority < b.priority)
                return -1;
            if (a.priority > b.priority)
                return 1;
            return 0;
        });
    }
    void Start() // стандартная функция Unity, запускается 1 раз в самом начале
	{ 
        DebugPanel.AddDelegate("Complete the level", () => {
            if (isPlaying) {
                reachedTheTarget = true;
                movesCount = 0;
                score = LevelProfile.main.thirdStarScore;
            }
        });
        DebugPanel.AddDelegate("Fail the level", () => {
            if (isPlaying) {
                reachedTheTarget = false;
                limitationRoutineIsOver = true;
                movesCount = 0;
            }
        });
        DebugPanel.AddDelegate("Add a bomb", () => {
            if (isPlaying) {
                List<string> powerups = chipInfos.Select(x => x.name).ToList();
                powerups.Remove("SimpleChip");
                if (powerups.Contains("Sugar"))
                    powerups.Remove("Sugar");
                if (powerups.Contains("Stone"))
                    powerups.Remove("Stone");
                FieldAssistant.main.AddPowerup(powerups[Random.Range(0, powerups.Count)]);
            }
        });
    }
    void OnApplicationPause(bool pauseStatus) // Функция, ставящая игру на паузу
	{ 
        if (isPlaying)
            UIAssistant.main.ShowPage("Pause");
    }
    
    public static void Reset() // Функция, устанавливающая значение переменых при старте нового уровня
	{  
		main.stars = 0;
        main.eventCount = 0;
        main.matchCount = 0;
        main.lastMovementId = 0;
        main.swapEvent = 0;
        main.score = 0;
        main.firstChipGeneration = true;
        main.isPlaying = false;
        main.movesCount = LevelProfile.main.limit;
        main.creatingSugarTask = 0;
        // добавленые данные
        Parametrs.main.Minus();
        Parametrs.main.MovesinGame = 0;
        StartNavel.main.Fin_Activ(false);
        main.reachedTheTarget = false;
        main.outOfLimit = false;
        main.targetRoutineIsOver = false;
        main.limitationRoutineIsOver = false;
        main.iteraction = true;
    }
    public void MovesFunction (int movess) // Функция, отнимающая параметры, когда игрок делает ход
    {
    eat -= movess;
    energy -= movess;
    coffee -= movess;
    hygiene -= movess;
   
    TimeMinute += 5 * movess;
    Parametrs.main.MovesinGame += movess;
    }
    public void MixChips(Chip a, Chip b) // Функция, меняющиая местами элементы 
	{  
        Mix mix = Mix.FindMix(a.chipType, b.chipType);
        if (mix == null)
            return;
        Chip target = null;
        Chip secondary = null;
        if (a.chipType == mix.pair.a) {
            target = a;
            secondary = b;
        }
        if (b.chipType == mix.pair.a) {
            target = b;
            secondary = a;
        }
        if (target == null) {
            Debug.LogError("It can't be mixed, because there is no target chip");
            return;
        }
        b.slot.chip = target;
        secondary.HideChip(false);
        target.SendMessage(mix.function, secondary);
    }
    public void Win_fifti() // Функция, выдающая победу с определенным шансом 
    {
        int chance = Random.Range(1, 11);
        if (score > LevelProfile.main.secondStarScore)
        {
            if (chance >= 4)
            {
                Debug.Log("Получилось 70 - "+ chance);
                StartCoroutine(GameCamera.main.HideFieldRoutine());
                FieldAssistant.main.RemoveField();
                StartCoroutine(YouWin());
            }
            else
            {
                Debug.Log("не получилось 70 - "+ chance);
                StartCoroutine(GameCamera.main.HideFieldRoutine());
                FieldAssistant.main.RemoveField();
                ShowLosePopup();
            }
        }
        else
        {
            if (chance >= 8)
            {
                Debug.Log("Получилось  35 - " + chance);
                StartCoroutine(GameCamera.main.HideFieldRoutine());
                FieldAssistant.main.RemoveField();
                StartCoroutine(YouWin());
            }
            else
            {
                Debug.Log("не получилось 35 - " + chance);
                StartCoroutine(GameCamera.main.HideFieldRoutine());
                FieldAssistant.main.RemoveField();
                ShowLosePopup();
            }
        }
    }
    public void Win() // Функция, запускающая вывод ифнормации о победе
    {
        AnimationAssistant.main.Ferverk(0);
        ChangeBG.main.UpdateStatusDevice();
        StopAllCoroutines();
        StartCoroutine(QuitCoroutine());
    }
    public void StartForLevel(int level) // функция запускающая уровень - level
    {
        Level.LoadMyLev(level);
    }
    public int GetBestPoints() //Получить лучший результат 
    {
        return ProfileAssistant.main.local_profile.GetScore(LevelProfile.main.level);
    }
    public int GetPoints() //Получить количество очков 
    {
        return score;
    }
    IEnumerator PlayLevelRoutine(int level)  // корутина, запускающая уровень
	{
        yield return StartCoroutine(QuitCoroutine());
        while (CPanel.uiAnimation > 0)
            yield return 0;
        Level.LoadMyLev(level);
    }
    public void RestartLevel()     // корутина, перезапускающая уровень
	{ 
        if (CPanel.uiAnimation > 0)
            return;
        StartCoroutine(PlayLevelRoutine(LevelProfile.main.level));
    }
   
    public void StartSession()  // Запускается при старте уровня
	{
        StopAllCoroutines();
        GameCamera.main.transform.position = new Vector3(0, 10, -10);
        GameCamera.cam.orthographicSize = 5;
        isPlaying = true;
        
        StartCoroutine(MovesLimitation());  // Завершение игры при обнулении ходов
        StartCoroutine(TargetSession(() => {  // Завершение игры при достижении цели
            return true;
        }));
        StartCoroutine(BaseSession()); 
        StartCoroutine(ShowingHintRoutine()); 
        StartCoroutine(ShuffleRoutine()); 
        StartCoroutine(FindingSolutionsRoutine()); 
        ChangeBG.main.ChengeArtAndBG(LevelProfile.main.level);
       GameCamera.main.ShowField();  // перемещает камеру к полю
        UIAssistant.main.ShowPage("Field");
    }
    IEnumerator BaseSession() // базовая куротина, проверающая победу или порожение
	{
        
        while (!limitationRoutineIsOver && !targetRoutineIsOver) { 
            yield return 0;
        }
        StartNavel.main.Fin_Activ(false);
        Debug.Log(limitationRoutineIsOver + "   " + targetRoutineIsOver);
        if (!reachedTheTarget) {
            
            yield return StartCoroutine(GameCamera.main.HideFieldRoutine());
            FieldAssistant.main.RemoveField();
            ShowLoseGamePopup();
            
            yield break;
        }
        iteraction = false;
        
        
        yield return new WaitForSeconds(0.2f);
        UIAssistant.main.ShowPage("TargetIsReached");
        AudioAssistant.Shot("TargetIsReached");
        yield return StartCoroutine(Utils.WaitFor(() => CPanel.uiAnimation == 0, 0.4f));
        UIAssistant.main.ShowPage("Field");
        
        yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
        
        yield return StartCoroutine(GameCamera.main.HideFieldRoutine());
        FieldAssistant.main.RemoveField();
        StartCoroutine(YouWin());
    }
    // Завершение игры при достижении цели
    IEnumerator TargetSession(System.Func<bool> success, System.Func<bool> failed = null)
    {
        reachedTheTarget = false;
        yield return 0;
        
        int score_target = LevelProfile.main.thirdStarScore;
        int score_fist_target = LevelProfile.main.firstStarScore;
        bool Fin_activ = false;
        while (!outOfLimit && (score < score_target || !success.Invoke()) && (failed == null || !failed.Invoke()))
        {
            if (score > score_fist_target && !Fin_activ)
            {
                Fin_activ = true;
                StartNavel.main.Fin_Activ(true);
            }
            
            yield return new WaitForSeconds(0.33f);
            if (movesCount <= 0 || eat <= 0 || energy <= 0 || hygiene <=0 || coffee <= 0)
            {
                StartNavel.main.Fin_Activ(false);
            }
        }
        if (score >= LevelProfile.main.firstStarScore && success.Invoke() && (failed == null || !failed.Invoke()))
            reachedTheTarget = true;
        Debug.Log("targetRoutineIsOver - "+movesCount + "  " + eat + "  " + energy);
        targetRoutineIsOver = true;
        
    }
    
    // Завершение игры при обнулении ходов
    IEnumerator MovesLimitation()
    {
        
        outOfLimit = false;

        while (movesCount > 0 && eat > 0 && energy > 0 && hygiene > 0 && coffee > 0)
            {
            
                yield return new WaitForSeconds(1f);
            if (movesCount <= 0 || eat <= 0 || energy <= 0 || hygiene <= 0 || coffee <= 0)
            {
                
                do
                {
                    yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
                }
                while (FindObjectsOfType<Chip>().ToList().Find(x => x.destroying) != null);
            }
        }
        
        yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
        Debug.Log(movesCount + "  " + eat + "  "+energy);
        limitationRoutineIsOver = true;
        outOfLimit = true;
        
    }
    // прогрмма поиска и уничтожения комбинаций соотвествующи правилам игры
    IEnumerator FindingSolutionsRoutine() {
        List<Solution> solutions;
        int id = 0;
        while (true) {
            if (isPlaying) {
                yield return StartCoroutine(Utils.WaitFor(() => lastMovementId > id, 0.2f));
                id = lastMovementId;
                solutions = FindSolutions();
                if (solutions.Count > 0) {
                    matchCount++;
                    MatchSolutions(solutions);
                } else
                    yield return StartCoroutine(Utils.WaitFor(() => {
                        return Chip.busyList.Count == 0;
                    }, 0.1f));
            } else
                yield return 0;
        }
    }

    // Выход из уровня
    public void Quit() {
        StopAllCoroutines();
        StartCoroutine(QuitCoroutine());
    }
    // программа выхода из уровня
    IEnumerator QuitCoroutine() {
        while (CPanel.uiAnimation > 0)
            yield return 0;
        isPlaying = false;
        if (GameCamera.main.playing) {
            UIAssistant.main.ShowPage("Field");
            yield return StartCoroutine(GameCamera.main.HideFieldRoutine());
            FieldAssistant.main.RemoveField();
            while (CPanel.uiAnimation > 0)
                yield return 0;
        }
        UIAssistant.main.ShowPage("Loading");
        while (CPanel.uiAnimation > 0)
            yield return 0;
        yield return new WaitForSeconds(0.5f);
        StartNavel.main.MessQuit();
    }
    // прогмаа активации бомб
    IEnumerator CollapseAllPowerups() {
        yield return StartCoroutine(Utils.WaitFor(CanIWait, 0.5f));
        List<Chip> powerUp = FindPowerups();
        while (powerUp.Count > 0) {
            powerUp = powerUp.FindAll(x => !x.destroying);
            if (powerUp.Count > 0) {
                EventCounter();
                Chip pu = powerUp[Random.Range(0, powerUp.Count)];
                pu.DestroyChip(); 
            }
            yield return StartCoroutine(Utils.WaitFor(CanIWait, 0.5f));
            powerUp = FindPowerups();
        }
        yield return StartCoroutine(Utils.WaitFor(CanIWait, 0.5f));
    }
    // нахождение необохдимой бомбы
    List<Chip> FindPowerups() {
        return FindObjectsOfType<IBomb>().Select(x => x.GetComponent<Chip>()).ToList();
    }
    // показыает всплывающее окно проигрыша в уровне
    void ShowLosePopup() {
        AudioAssistant.Shot("YouLose");
        isPlaying = false;
        GameCamera.main.HideField();
        UIAssistant.main.ShowPage("YouLose");
    }
    // проигрышь в игре
    void ShowLoseGamePopup()
    {
        AudioAssistant.Shot("YouLose");
        isPlaying = false;
        GameCamera.main.HideField();
        UIAssistant.main.ShowPage("YouLoseGame");
    } 
    // прогрмма,запускающая определеные деуйствия при выйгрыше в игре
    IEnumerator YouWin() 
	{
        AudioAssistant.Shot("YouWin");
        SteamAchiments.main.TakeAchievement("WIN_GAME");
        PlayerPrefs.SetInt("FirstPass", 1);
        isPlaying = false;
        
        if (ProfileAssistant.main.local_profile.current_level == LevelProfile.main.level)
            if (Level.all.ContainsKey(ProfileAssistant.main.local_profile.current_level + 1))
                ProfileAssistant.main.local_profile.current_level++;
        ProfileAssistant.main.local_profile.SetScore(LevelProfile.main.level, score);
        GameCamera.main.HideField();
        yield return 0;
        while (CPanel.uiAnimation > 0)
            yield return 0;
        yield return 0;
        UIAssistant.main.ShowPage("YouWin");
        Debug.Log("YouWin");
        AnimationAssistant.main.Ferverk(1);
        UserProfileUtils.WriteProfileOnDevice(ProfileAssistant.main.local_profile);
    }
     // показывает окно выйгрыша
	public void ShowYouWinPopup() {
        bool bestScore = false;
        if (ProfileAssistant.main.local_profile.GetScore(LevelProfile.main.level) < score)
        {
            ProfileAssistant.main.local_profile.SetScore(LevelProfile.main.level, score);
            bestScore = true;
        }
        UIAssistant.main.ShowPage(bestScore ? "YouWinBestScore" : "YouWin");
    }
    // уловия ожидания действий игрока
    public bool CanIWait() {
        return isPlaying && Chip.busyList.Count == 0;
    }
    // счетчик действий 
    public void EventCounter() {
        eventCount++;
    }
    // фукция поиска возможных ходов
    public List<Move> FindMoves() {
        List<Move> moves = new List<Move>();
        if (!FieldAssistant.main.gameObject.activeSelf)
            return moves;
        if (LevelProfile.main == null)
            return moves;
        Solution solution;
        int potential;
        Side[] asixes = new Side[2] { Side.Right, Side.Top };
        foreach (Side asix in asixes) {
            foreach (Slot slot in Slot.all.Values) {
                if (slot[asix] == null)
                    continue;
                if (slot.block != null || slot[asix].block != null)
                    continue;
                if (slot.chip == null || slot[asix].chip == null)
                    continue;
                if (slot.chip.id == slot[asix].chip.id)
                    continue;
                Move move = new Move();
                move.from = slot.coord;
                move.to = slot[asix].coord;
                AnalizSwap(move);
                Dictionary<Slot, Solution> solutions = new Dictionary<Slot, Solution>();
                Slot[] cslots = new Slot[2] { slot, slot[asix] };
                foreach (Slot cslot in cslots) {
                    solutions.Add(cslot, null);
                    potential = 0;
                    solution = MatchAnaliz(cslot);
                    if (solution != null) {
                        solutions[cslot] = solution;
                        potential = solution.potential;
                    }
                    solution = MatchSquareAnaliz(cslot);
                    if (solution != null && potential < solution.potential) {
                        solutions[cslot] = solution;
                        potential = solution.potential;
                    }
                    move.potencial += potential;
                }
                if (solutions[cslots[0]] != null && solutions[cslots[1]] != null)
                    move.solution = solutions[cslots[0]].potential > solutions[cslots[1]].potential ? solutions[cslots[0]] : solutions[cslots[1]];
                else
                    move.solution = solutions[cslots[0]] != null ? solutions[cslots[0]] : solutions[cslots[1]];
                AnalizSwap(move);
                if (Mix.ContainsThisMix(slot.chip.chipType, slot[asix].chip.chipType))
                    move.potencial += 100;
                if (move.potencial > 0)
                    moves.Add(move);
            }
        }
        return moves;
    }
    // меняет местами два элемента в соответствии с ходом
    void AnalizSwap(Move move) {
        Slot slot;
        Chip fChip = Slot.GetSlot(move.from).chip;
        Chip tChip = Slot.GetSlot(move.to).chip;
        if (!fChip || !tChip)
            return;
        slot = tChip.slot;
        fChip.slot.chip = tChip;
        slot.chip = fChip;
    }
    // проверяет, если ли 3 или более элементов в одном ряду и удалает их
	void MatchSolutions(List<Solution> solutions) {
        
        if (!isPlaying)
            return;
        solutions.Sort(delegate (Solution x, Solution y) {
            if (x.potential == y.potential)
                return 0;
            else if (x.potential > y.potential)
                return -1;
            else
                return 1;
        });
        int width = LevelProfile.main.width;
        int height = LevelProfile.main.height;
        bool[,] mask = new bool[width, height];
        int2 key = new int2();
        Slot slot;
        for (key.x = 0; key.x < width; key.x++)
            for (key.y = 0; key.y < height; key.y++) {
                mask[key.x, key.y] = false;
                if (Slot.all.ContainsKey(key)) {
                    slot = Slot.all[key];
                    if (slot.chip)
                        mask[key.x, key.y] = true;
                }
            }
        List<Solution> final_solutions = new List<Solution>();
        bool breaker;
        foreach (Solution s in solutions) {
            breaker = false;
            foreach (Chip c in s.chips) {
                if (!mask[c.slot.x, c.slot.y]) {
                    breaker = true;
                    break;
                }
            }
            if (breaker)
                continue;
            final_solutions.Add(s);
            foreach (Chip c in s.chips)
                mask[c.slot.x, c.slot.y] = false;
        }
        foreach (Solution solution in final_solutions) {
            EventCounter();
            int puID = -1;
            if (solution.chips.Count(x => !x.IsMatcheble()) == 0) {
                int destroy = 0;
                foreach (Chip chip in solution.chips) {
                    if (chip.id == solution.id || chip.IsUniversalColor()) {
                        if (!chip.slot)
                            continue;
                        slot = chip.slot;
                        if (chip.movementID > puID)
                            puID = chip.movementID;
                        chip.SetScore(Mathf.Pow(2, solution.count - 3) / solution.count);
                        if (!slot.block)
                            FieldAssistant.main.BlockCrush(slot.coord, true);
                        destroy++;
                        
                        chip.DestroyChip();
                    }
                }
                Debug.Log("Destroy Chip - "+ destroy); 
            } else
                return;
            solution.chips.Sort(delegate (Chip a, Chip b) {
                return a.movementID > b.movementID ? -1 : a.movementID == b.movementID ? 0 : 1;
            });
            breaker = false;
            foreach (Combinations combination in combinations) {
                if (combination.square && !solution.q)
                    continue;
                if (!combination.square) {
                    if (combination.horizontal && !solution.h)
                        continue;
                    if (combination.vertical && !solution.v)
                        continue;
                    if (combination.minCount > solution.count)
                        continue;
                }
                foreach (Chip ch in solution.chips)
                    if (ch.chipType == "SimpleChip") {
                        FieldAssistant.main.GetNewBomb(ch.slot.coord, combination.chip, ch.slot.transform.position, solution.id);
                        breaker = true;
                        break;
                    }
                if (breaker)
                    break;
            }
        }
    }
    // получить количество ходов
	public int GetMovesCount() {
        return movesCount;
    }
       // получить количество очков
	public float GetResource() {
         return (1f * movesCount *eat * energy*coffee*hygiene) / LevelProfile.main.limit;
    }
    // меняет элементы местами, если нет ходов 
    IEnumerator ShuffleRoutine() {
        int shuffleOrder = 0;
        float delay = 1;
        while (true) {
            yield return StartCoroutine(Utils.WaitFor(CanIWait, delay));
            if (eventCount > shuffleOrder && !targetRoutineIsOver && Chip.busyList.Count == 0) {
                shuffleOrder = eventCount;
                yield return StartCoroutine(Shuffle(false));
            }
        }
    }
    // програма меняющая местами элементы
    public IEnumerator Shuffle(bool f) {
        bool force = f;
        List<Move> moves = FindMoves();
        if (moves.Count > 0 && !force)
            yield break;
        if (!isPlaying)
            yield break;
        isPlaying = false;
        List<Slot> slots = new List<Slot>(Slot.all.Values);
        Dictionary<Slot, Vector3> positions = new Dictionary<Slot, Vector3>();
        foreach (Slot slot in slots)
            positions.Add(slot, slot.transform.position);
        float t = 0;
        while (t < 1) {
            t += Time.unscaledDeltaTime * 3;
            Slot.folder.transform.localScale = Vector3.one * Mathf.Lerp(1, 0.6f, EasingFunctions.easeInOutQuad(t));
            Slot.folder.transform.eulerAngles = Vector3.forward * Mathf.Lerp(0, Mathf.Sin(Time.unscaledTime * 40) * 3, EasingFunctions.easeInOutQuad(t));
            yield return 0;
        }
        if (f || moves.Count == 0) {
            f = false;
            RawShuffle(slots);
        }
        moves = FindMoves();
        List<Solution> solutions = FindSolutions();
        int itrn = 0;
        int targetID;
        while (solutions.Count > 0 || moves.Count == 0) {
            if (itrn > 100) {
                ShowLosePopup();
                yield break;
            }
            if (solutions.Count > 0) {
                for (int s = 0; s < solutions.Count; s++) {
                    targetID = Random.Range(0, slots.Count - 1);
                    if (slots[targetID].chip && slots[targetID].chip.chipType != "Sugar" && slots[targetID].chip.id != solutions[s].id)
                        Swap(solutions[s].chips[Random.Range(0, solutions[s].chips.Count - 1)], slots[targetID].chip);
                }
            } else
                RawShuffle(slots);
            moves = FindMoves();
            solutions = FindSolutions();
            itrn++;
            Slot.folder.transform.eulerAngles = Vector3.forward * Mathf.Sin(Time.unscaledTime * 40) * 3;
            yield return 0;
        }
        t = 0;
        AudioAssistant.Shot("Shuffle");
        while (t < 1) {
            t += Time.unscaledDeltaTime * 3;
            Slot.folder.transform.localScale = Vector3.one * Mathf.Lerp(0.6f, 1, EasingFunctions.easeInOutQuad(t));
            Slot.folder.transform.eulerAngles = Vector3.forward * Mathf.Lerp(Mathf.Sin(Time.unscaledTime * 40) * 3, 0, EasingFunctions.easeInOutQuad(t));
            yield return 0;
        }
        Slot.folder.transform.localScale = Vector3.one;
        Slot.folder.transform.eulerAngles = Vector3.zero;
        isPlaying = true;
    }
    // функция поиска возможных решений
    List<Solution> FindSolutions() {
        List<Solution> solutions = new List<Solution>();
        Solution zsolution;
        foreach (Slot slot in Slot.all.Values) {
            zsolution = MatchAnaliz(slot);
            if (zsolution != null)
                solutions.Add(zsolution);
            zsolution = MatchSquareAnaliz(slot);
            if (zsolution != null)
                solutions.Add(zsolution);
        }
        return solutions;
    }
    // показывает подсказки
    IEnumerator ShowingHintRoutine() {
        int hintOrder = 0;
        float delay = 5;
        yield return new WaitForSeconds(1f);
        while (!reachedTheTarget) {
            while (!isPlaying)
                yield return 0;
            yield return StartCoroutine(Utils.WaitFor(CanIWait, delay));
            if (eventCount > hintOrder) {
                hintOrder = eventCount;
                ShowHint();
            }
        }
    }
	// меняет 2 элемента местами
    IEnumerator SwapByPlayerRoutine(Chip a, Chip b, bool onlyForMatching, bool byAI = false) {
        System.Action breaker = () => {
            if (a && !a.destroying)
                a.busy = false;
            if (b && !b.destroying)
                b.busy = false;
            swaping = false;
        };
        if (!isPlaying)
            yield break;
        if (!iteraction && !byAI)
            yield break;
        
        if (swaping)
            yield break; 
        if (!a || !b)
            yield break; 
        if (a.destroying || b.destroying)
            yield break;
        if (Chip.busyList.Count > 0)
            yield break;
        
        if (a.slot.block || b.slot.block)
            yield break; 
        if (movesCount <= 0 ||eat <= 0 || energy <= 0 || hygiene <= 0 || coffee <= 0)
            yield break;
        Mix mix = mixes.Find(x => x.Compare(a.chipType, b.chipType));
        int move = 0; 
        swaping = true;
        Vector3 posA = a.slot.transform.position;
        Vector3 posB = b.slot.transform.position;
        float progress = 0;
        Vector3 normal = a.slot.x == b.slot.x ? Vector3.right : Vector3.up;
        float time = 0;
        a.busy = true;
        b.busy = true;
        
        while (progress < ProjectParameters.main.swap_duration) {
            if (!a || !b) {
                breaker.Invoke();
                yield break;
            }
            time = EasingFunctions.easeInOutQuad(progress / ProjectParameters.main.swap_duration);
            a.transform.position = Vector3.Lerp(posA, posB, time) + normal * Mathf.Sin(3.14f * time) * 0.2f;
            if (mix == null)
                b.transform.position = Vector3.Lerp(posB, posA, time) - normal * Mathf.Sin(3.14f * time) * 0.2f;
            progress += Time.deltaTime;
            yield return 0;
        }
        a.transform.position = posB;
        if (mix == null)
            b.transform.position = posA;
        a.movementID = main.GetMovementID();
        b.movementID = main.GetMovementID();
        if (mix != null) { 
            swaping = false;
            a.busy = false;
            b.busy = false;
            MixChips(a, b);
            yield return new WaitForSeconds(0.3f);
            MovesFunction(1);
            swapEvent++;
            yield break;
        }
        
        Slot slotA = a.slot;
        Slot slotB = b.slot;
        if (!slotB || !slotB || !a || !b) {
            breaker.Invoke();
            yield break;
        }
        slotB.chip = a;
        slotA.chip = b;
        move++;
        
        int count = 0;
        Solution solution;
        solution = MatchAnaliz(slotA);
        if (solution != null)
            count += solution.count;
        solution = MatchSquareAnaliz(slotA);
        if (solution != null)
            count += solution.count;
        solution = MatchAnaliz(slotB);
        if (solution != null)
            count += solution.count;
        solution = MatchSquareAnaliz(slotB);
        if (solution != null)
            count += solution.count;
        
        if (count == 0 && !onlyForMatching) {
            AudioAssistant.Shot("SwapFailed");
            while (progress > 0) {
                if (!a || !b) {
                    breaker.Invoke();
                    yield break;
                }
                time = EasingFunctions.easeInOutQuad(progress / ProjectParameters.main.swap_duration);
                a.transform.position = Vector3.Lerp(posA, posB, time) - normal * Mathf.Sin(3.14f * time) * 0.2f;
                b.transform.position = Vector3.Lerp(posB, posA, time) + normal * Mathf.Sin(3.14f * time) * 0.2f;
                progress -= Time.deltaTime;
                yield return 0;
            }
            a.transform.position = posA;
            b.transform.position = posB;
            a.movementID = GetMovementID();
            b.movementID = GetMovementID();
            slotB.chip = b;
            slotA.chip = a;
            move--;
        } else {
            AudioAssistant.Shot("SwapSuccess");
            swapEvent++;
        }
        firstChipGeneration = false;
        if (!byAI)
        {
            Debug.Log("move = " + move);
            MovesFunction(move);
        }
          
        EventCounter();
        a.busy = false;
        b.busy = false;
        swaping = false;
    }
	

	
	
	
	
	
    public Solution MatchAnaliz(Slot slot) {
        if (!slot.chip)
            return null;
        if (!slot.chip.IsMatcheble())
            return null;
        if (slot.chip.IsUniversalColor()) { 
            List<Solution> solutions = new List<Solution>();
            Solution z;
            Chip multicolorChip = slot.chip;
            for (int i = 0; i < 6; i++) {
                multicolorChip.id = i;
                z = MatchAnaliz(slot);
                if (z != null)
                    solutions.Add(z);
                z = MatchSquareAnaliz(slot);
                if (z != null)
                    solutions.Add(z);
            }
            multicolorChip.id = Chip.universalColorId;
            z = null;
            foreach (Solution sol in solutions)
                if (z == null || z.potential < sol.potential)
                    z = sol;
            return z;
        }
        Slot s;
        Dictionary<Side, List<Chip>> sides = new Dictionary<Side, List<Chip>>();
        int count;
        int2 key;
        foreach (Side side in Utils.straightSides) {
            count = 1;
            sides.Add(side, new List<Chip>());
            while (true) {
                key = slot.coord + Utils.SideOffset(side) * count;
                if (!Slot.all.ContainsKey(key))
                    break;
                s = Slot.all[key];
                if (!s.chip)
                    break;
                if (s.chip.id != slot.chip.id && !s.chip.IsUniversalColor())
                    break;
                if (!s.chip.IsMatcheble())
                    break;
                sides[side].Add(s.chip);
                count++;
            }
        }
        bool h = sides[Side.Right].Count + sides[Side.Left].Count >= 2;
        bool v = sides[Side.Top].Count + sides[Side.Bottom].Count >= 2;
        if (h || v) {
            Solution solution = new Solution();
            solution.h = h;
            solution.v = v;
            solution.chips = new List<Chip>();
            solution.chips.Add(slot.chip);
            if (h) {
                solution.chips.AddRange(sides[Side.Right]);
                solution.chips.AddRange(sides[Side.Left]);
            }
            if (v) {
                solution.chips.AddRange(sides[Side.Top]);
                solution.chips.AddRange(sides[Side.Bottom]);
            }
            solution.count = solution.chips.Count;
            solution.x = slot.x;
            solution.y = slot.y;
            solution.id = slot.chip.id;
            foreach (Chip c in solution.chips)
                solution.potential += c.GetPotencial();
            return solution;
        }
        return null;
    }
    public Solution MatchSquareAnaliz(Slot slot) {
        if (!slot.chip)
            return null;
        if (!slot.chip.IsMatcheble())
            return null;
        if (slot.chip.IsUniversalColor()) { 
            List<Solution> solutions = new List<Solution>();
            Solution z;
            Chip multicolorChip = slot.chip;
            for (int i = 0; i < 6; i++) {
                multicolorChip.id = i;
                z = MatchSquareAnaliz(slot);
                if (z != null)
                    solutions.Add(z);
            }
            multicolorChip.id = Chip.universalColorId;
            z = null;
            foreach (Solution sol in solutions)
                if (z == null || z.potential < sol.potential)
                    z = sol;
            return z;
        }
        List<Chip> square = new List<Chip>();
        List<Chip> buffer = new List<Chip>();
        Side sideR;
        int2 key;
        Slot s;
        buffer.Clear();
        foreach (Side side in Utils.straightSides) {
            for (int r = 0; r <= 2; r++) {
                sideR = Utils.RotateSide(side, r);
                key = slot.coord + Utils.SideOffset(sideR);
                if (Slot.all.ContainsKey(key)) {
                    s = Slot.all[key];
                    if (s.chip && (s.chip.id == slot.chip.id || s.chip.IsUniversalColor()) && s.chip.IsMatcheble())
                        buffer.Add(s.chip);
                    else
                        break;
                } else
                    break;
            }
            if (buffer.Count == 3) {
                foreach (Chip chip_b in buffer)
                    if (!square.Contains(chip_b))
                        square.Add(chip_b);
            }
            buffer.Clear();
        }
        bool q = square.Count >= 3;
        if (q) {
            Solution solution = new Solution();
            solution.q = q;
            solution.chips = new List<Chip>();
            solution.chips.Add(slot.chip);
            solution.chips.AddRange(square);
            solution.count = solution.chips.Count;
            solution.x = slot.x;
            solution.y = slot.y;
            solution.id = slot.chip.id;
            foreach (Chip c in solution.chips)
                solution.potential += c.GetPotencial();
            return solution;
        }
        return null;
    }
    
    bool swaping = false; 
    public bool iteraction = false;
    
    public void Swap(Chip a, Chip b) {
        if (!a || !b)
            return;
        if (a == b)
            return;
        if (a.slot.block || b.slot.block)
            return;
        a.movementID = GetMovementID();
        b.movementID = GetMovementID();
        Slot slotA = a.slot;
        Slot slotB = b.slot;
        slotB.chip = a;
        slotA.chip = b;
    }
    
    public void SwapByPlayer(Chip a, Chip b, bool onlyForMatching, bool byAI = false) {
        StartCoroutine(SwapByPlayerRoutine(a, b, onlyForMatching, byAI)); 
    }
    public void SwapByPlayer(Move move, bool onlyForMatching, bool byAI = false)
    {
       
        Chip a = Slot.all[move.from].chip;
        Chip b = Slot.all[move.to].chip;
        if (a && b)
            SwapByPlayer(a, b, onlyForMatching, byAI);
    
        }
    
    
    void ShowHint() {
        if (!isPlaying)
            return;
        List<Move> moves = FindMoves();
        foreach (Move move in moves) {
            Debug.DrawLine(Slot.GetSlot(move.from).transform.position, Slot.GetSlot(move.to).transform.position, Color.red, 10);
        }
        if (moves.Count == 0)
            return;
        Move bestMove = moves[Random.Range(0, moves.Count)];
        if (bestMove.solution == null) {
            Slot.GetSlot(bestMove.from).chip.Flashing(eventCount);
            Slot.GetSlot(bestMove.to).chip.Flashing(eventCount);
        } else
            foreach (Chip chip in bestMove.solution.chips)
                chip.Flashing(eventCount);
    }
    [System.Serializable]
    public class ChipInfo {
        public string name = "";
        public string contentName = "";
        public bool color = true;
        public string shirtName = "";
    }
    [System.Serializable]
    public class BlockInfo {
        public string name = "";
        public string contentName = "";
        public string shirtName = "";
        public int levelCount = 0;
        public bool chip = false;
    }
    [System.Serializable]
    public class Combinations {
        public int priority = 0;
        public string chip;
        public bool horizontal = true;
        public bool vertical = true;
        public bool square = false;
        public int minCount = 4;
    }
    
    public class Solution {

        public int count; 
        public int potential; 
        public int id; 
        public List<Chip> chips = new List<Chip>();
        
        public int x;
        public int y;
        public bool v; 
        public bool h; 
        public bool q;
        //public int posV; 
        //public int negV; 
        //public int posH; 
        //public int negH; 
    }
    
    public class Move {
        
        
        
        
        public int2 from;
        
        public int2 to;
        public Solution solution; 
        public int potencial; 
    }
   
    public class Mix // Класс, меняющий местами элементы 
	{
        public Pair pair = new Pair("", "");
        public string function;
        public bool Compare(string _a, string _b) {
            return pair == new Pair(_a, _b);
        }
        public static bool ContainsThisMix(string _a, string _b) {
            return main.mixes.Find(x => x.Compare(_a, _b)) != null;
        }
        public static Mix FindMix(string _a, string _b) {
            return main.mixes.Find(x => x.Compare(_a, _b));
        }
    }
	
	
    public class Pair 
	{
        public string a;
        public string b;
        public Pair(string pa, string pb) {
            a = pa;
            b = pb;
        }
        public static bool operator ==(Pair a, Pair b) 
		{
            return Equals(a, b);
        }
        public static bool operator !=(Pair a, Pair b) 
		{
            return !Equals(a, b);
        }
        public override bool Equals(object obj) 
		{
            Pair sec = (Pair) obj;
            return (a.Equals(sec.a) && b.Equals(sec.b)) ||
                (a.Equals(sec.b) && b.Equals(sec.a));
        }
        public override int GetHashCode() 
		{
            return a.GetHashCode() + b.GetHashCode();
        }
    }
}