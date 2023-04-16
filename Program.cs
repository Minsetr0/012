const int MaxPlayerHP = 15;
const int MaxLuciferHP = 30;
const int MaxRafaelHP = 20;
const string ElectricCageSpellNumber = "1";
const string ElectricCageSpellName = "Electric Cage";
const int ElectricCagePercentToFinishing = 40;
const int ElectricCageVarPlayerLostHP = 5;
const string HolyRaySpellNumber = "2";
const string HolyRaySpellName = "Holy Ray";
const int HolyRayDamage = 4;
const int HolyRayDamageToPlayer = 8;
const string ElectricRaySpellNumber = "3";
const string ElectricRaySpellName = "Electric Ray";
const int ElectricRayDamage = 1;
const int ElectricRayDamageStack = 1;
const string RainOfPoisonArrowsSpellNumber = "4";
const string RainOfPoisonArrowsSpellName = "Rain of Poison Arrows";
const int RainOfPoisonArrowsDamage = 3;
const int RainOfPoisonArrowsTimeDamage = 2;
const string HolyWaterSpellNumber = "5";
const string HolyWaterSpellName = "Holy Water";
const string DemonType = "[Демон]";
const string PoisonType = "[Ядовитый]";
const string AngelType = "[Ангел]";
const string ChoiseToLucifer = "1";
const string ChoiseToRafael = "2";
const int RafaelAuraDamage = 2;

int bossHP = 30;
int playerHP = 15;
int lifeForDeath = 0;
int useToHolyWater = 2;
int stacksInElectricRay = 0;
bool bossDemonType = false;
bool bossPoisonType = false;
bool bossAngelType = false;
bool poisonAttackToPlayer = false;
bool poisonAttackToBoss = false;
string bossChoice = "0";
int maxBossHP = 0;

while (bossChoice != "1" && bossChoice != "2")
{
    Console.WriteLine($"Выберите, против какого босса вы хотите сражаться.\n1: Люцифер, имеет тип {DemonType} и {PoisonType}, {MaxLuciferHP} хп и заклинание {RainOfPoisonArrowsSpellName}" +
    $", у которой {RainOfPoisonArrowsDamage} урона и {RainOfPoisonArrowsTimeDamage} продолжительного урона." +
    $"\n2: Рафаэль, имеет тип {AngelType}, {MaxRafaelHP} хп и заклинание {HolyWaterSpellName}, которая постоянно восстанавливает себе все хп, для неё использования неограничены," +
    $" так же, у неё есть аура, которая наносит на поле боя 2 урона всем её врагам.");
    bossChoice = Console.ReadLine();

    switch (bossChoice)
    {
        case ChoiseToLucifer:
            bossDemonType = true;
            bossPoisonType = true;
            maxBossHP = MaxLuciferHP;
            bossHP = maxBossHP;
            break;
        case ChoiseToRafael:
            bossAngelType = true;
            maxBossHP = MaxRafaelHP;
            bossHP = maxBossHP;
            break;
        default:
            Console.WriteLine("Такого номера на выбор тебе не давал");
            break;
    }

    Console.Clear();
}

while (bossHP > lifeForDeath && playerHP > lifeForDeath)
{
    Console.WriteLine($"У вас {playerHP} хп. У босса {bossHP} хп.");
    Console.WriteLine($"Выберите, какое заклинание вы хотите использовать." +
        $"\n{ElectricCageSpellNumber}: {ElectricCageSpellName} - добивает цель, если у той осталось {ElectricCagePercentToFinishing}% или меньше здоровья, иначе не работает." +
        $" Вы теряете {ElectricCageVarPlayerLostHP} хп" +
        $"\n{HolyRaySpellNumber}: {HolyRaySpellName} - наносит цели {HolyRayDamage} урона, так же, если цель {DemonType}," +
        $" наносит урон еще раз, а если {AngelType}, вы теряете {HolyRayDamageToPlayer} хп." +
        $"\n{ElectricRaySpellNumber}: {ElectricRaySpellName} - наносит цели {ElectricRayDamage} урон." +
        $" Так же, цель всегда получает на {ElectricRayDamageStack} урон больше, но не от этого заклинания, так же не работает на урон по себе." +
        $"\n{RainOfPoisonArrowsSpellNumber}: {RainOfPoisonArrowsSpellName} - наносит цели {RainOfPoisonArrowsDamage} урон," +
        $" так же, если цель не {PoisonType}, цель теряет по {RainOfPoisonArrowsTimeDamage} хп каждый ваш ход. Не стакается." +
        $"\n{HolyWaterSpellNumber}: {HolyWaterSpellName} - вы полностью исцеляетесь. Можно использовать {useToHolyWater} раз.");
    string chooseToSpell = Console.ReadLine();

    switch (chooseToSpell)
    {
        case ElectricCageSpellNumber:
            float checkPercentHP = (maxBossHP / 100.0f) * ElectricCagePercentToFinishing;

            if (bossHP <= checkPercentHP)
            {
                bossHP = lifeForDeath;
                Console.WriteLine("Вы успешно добили босса!!!");
            }
            else if (bossHP > checkPercentHP)
            {
                Console.WriteLine($"Вы не смогли добить босса, у него больше, чем {ElectricCagePercentToFinishing}% хп.");
            }
            playerHP -= ElectricCageVarPlayerLostHP;
            Console.WriteLine($"Вы потеряли {ElectricCageVarPlayerLostHP} хп.");
            break;
        case HolyRaySpellNumber:
            bossHP -= (HolyRayDamage+ stacksInElectricRay);

            if (bossDemonType)
            {
                bossHP -= (HolyRayDamage + stacksInElectricRay);
            }
            else if (bossAngelType)
            { 
                playerHP -= HolyRayDamageToPlayer;
            }
            break;
        case ElectricRaySpellNumber:
            bossHP -= ElectricRayDamage;
            stacksInElectricRay += ElectricRayDamageStack;
            break;
        case RainOfPoisonArrowsSpellNumber:
            bossHP -= (RainOfPoisonArrowsDamage + stacksInElectricRay);

            if (bossPoisonType == false)
            {
                poisonAttackToBoss = true;
            }
            break;
        case HolyWaterSpellNumber:
            playerHP = MaxPlayerHP;
            useToHolyWater -= 1;
            break;
        default:
            Console.WriteLine("Нет такого заклинания, вы потратили ход в пустую");
            break;
    }

    if (bossDemonType)
    {
        playerHP -= RainOfPoisonArrowsDamage;
        poisonAttackToPlayer = true;
    }
    else if (bossAngelType)
    {
        playerHP -= RafaelAuraDamage;
        bossHP = maxBossHP;
    }  

    if (poisonAttackToBoss)
    {
        bossHP -= (RainOfPoisonArrowsTimeDamage + stacksInElectricRay);
    }
    
    if (poisonAttackToPlayer)
    {
        playerHP -= RainOfPoisonArrowsTimeDamage;
    }

    Console.Clear();
}

if (bossHP <= lifeForDeath && playerHP <= lifeForDeath)
{
    Console.WriteLine("Поздравляем вас, босс умер!!! Впрочем, как и вы.");
}
else if (bossHP <= lifeForDeath)
{
    Console.WriteLine("Поздравляем вас, босс умер!!! Игра окончена, вы победили.");
}
else if (playerHP <= lifeForDeath)
{
    Console.WriteLine("Вы умерли. Хорошая попытка.");
}