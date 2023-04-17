const string ComandElectricCageSpellNumber = "1";
const string ComandHolyRaySpellNumber = "2";
const string ComandElectricRaySpellNumber = "3";
const string ComandRainOfPoisonArrowsSpellNumber = "4";
const string ComandHolyWaterSpellNumber = "5";
const string ComandChoiseToLucifer = "1";
const string ComandChoiseToRafael = "2";

int maxPlayerHP = 15;
int maxLuciferHP = 30;
int maxRafaelHP = 20;
int bossHP = 30;
int playerHP = 15;
int lifeForDeath = 0;
int useToHolyWater = 2;
int stacksInElectricRay = 0;
bool isbossDemonType = false;
bool isbossPoisonType = false;
bool isbossAngelType = false;
bool ispoisonAttackToPlayer = false;
bool ispoisonAttackToBoss = false;
string bossChoice = "0";
int maxBossHP = 0;
string electricCageSpellName = "Electric Cage";
string electricCagePercentToFinishing = "40";
int electricCageVarPlayerLostHP = 5;
string holyRaySpellName = "Holy Ray";
int holyRayDamage = 4;
int holyRayDamageToPlayer = 8;
string electricRaySpellName = "Electric Ray";
int electricRayDamage = 1;
int electricRayDamageStack = 1;
string rainOfPoisonArrowsSpellName = "Rain of Poison Arrows";
int rainOfPoisonArrowsDamage = 3;
int rainOfPoisonArrowsTimeDamage = 2;
string holyWaterSpellName = "Holy Water";
string demonType = "[Демон]";
string poisonType = "[Ядовитый]";
string angelType = "[Ангел]";
int rafaelAuraDamage = 2;
float electricCageFinishing = 0.4f;

while (bossChoice != ComandChoiseToLucifer && bossChoice != ComandChoiseToRafael)
{
    Console.WriteLine($"Выберите, против какого босса вы хотите сражаться.\n{ComandChoiseToLucifer}: Люцифер, имеет тип {demonType} и {poisonType}, {maxLuciferHP} хп и заклинание {rainOfPoisonArrowsSpellName}" +
    $", у которой {rainOfPoisonArrowsDamage} урона и {rainOfPoisonArrowsTimeDamage} продолжительного урона." +
    $"\n{ComandChoiseToRafael}: Рафаэль, имеет тип {angelType}, {maxRafaelHP} хп и заклинание {holyWaterSpellName}, которая постоянно восстанавливает себе все хп, для неё использования неограничены," +
    $" так же, у неё есть аура, которая наносит на поле боя 2 урона всем её врагам.");
    bossChoice = Console.ReadLine();

    switch (bossChoice)
    {
        case ComandChoiseToLucifer:
            isbossDemonType = true;
            isbossPoisonType = true;
            maxBossHP = maxLuciferHP;
            bossHP = maxBossHP;
            break;

        case ComandChoiseToRafael:
            isbossAngelType = true;
            maxBossHP = maxRafaelHP;
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
        $"\n{ComandElectricCageSpellNumber}: {electricCageSpellName} - добивает цель, если у той осталось {electricCagePercentToFinishing}% или меньше здоровья, иначе не работает." +
        $" Вы теряете {electricCageVarPlayerLostHP} хп" +
        $"\n{ComandHolyRaySpellNumber}: {holyRaySpellName} - наносит цели {holyRayDamage} урона, так же, если цель {demonType}," +
        $" наносит урон еще раз, а если {angelType}, вы теряете {holyRayDamageToPlayer} хп." +
        $"\n{ComandElectricRaySpellNumber}: {electricRaySpellName} - наносит цели {electricRayDamage} урон." +
        $" Так же, цель всегда получает на {electricRayDamageStack} урон больше, но не от этого заклинания, так же не работает на урон по себе." +
        $"\n{ComandRainOfPoisonArrowsSpellNumber}: {rainOfPoisonArrowsSpellName} - наносит цели {rainOfPoisonArrowsDamage} урон," +
        $" так же, если цель не {poisonType}, цель теряет по {rainOfPoisonArrowsTimeDamage} хп каждый ваш ход. Не стакается." +
        $"\n{ComandHolyWaterSpellNumber}: {holyWaterSpellName} - вы полностью исцеляетесь. Можно использовать {useToHolyWater} раз.");
    string chooseToSpell = Console.ReadLine();

    switch (chooseToSpell)
    {
        case ComandElectricCageSpellNumber:
            float checkPercentHP = maxBossHP * electricCageFinishing;

            if (bossHP <= checkPercentHP)
            {
                bossHP = lifeForDeath;
                Console.WriteLine("Вы успешно добили босса!!!");
            }
            else if (bossHP > checkPercentHP)
            {
                Console.WriteLine($"Вы не смогли добить босса, у него больше, чем {electricCagePercentToFinishing}% хп.");
            }

            playerHP -= electricCageVarPlayerLostHP;
            Console.WriteLine($"Вы потеряли {electricCageVarPlayerLostHP} хп.");
            break;

        case ComandHolyRaySpellNumber:
            bossHP -= holyRayDamage+ stacksInElectricRay;

            if (isbossDemonType)
            {
                bossHP -= holyRayDamage + stacksInElectricRay;
            }
            else if (isbossAngelType)
            { 
                playerHP -= holyRayDamageToPlayer;
            }

            break;

        case ComandElectricRaySpellNumber:
            bossHP -= electricRayDamage;
            stacksInElectricRay += electricRayDamageStack;
            break;

        case ComandRainOfPoisonArrowsSpellNumber:
            bossHP -= rainOfPoisonArrowsDamage + stacksInElectricRay;

            if (isbossPoisonType == false)
            {
                ispoisonAttackToBoss = true;
            }

            break;

        case ComandHolyWaterSpellNumber:
            playerHP = maxPlayerHP;
            useToHolyWater -= 1;
            break;

        default:
            Console.WriteLine("Нет такого заклинания, вы потратили ход в пустую");
            break;
    }

    if (isbossDemonType)
    {
        playerHP -= rainOfPoisonArrowsDamage;
        ispoisonAttackToPlayer = true;
    }
    else if (isbossAngelType)
    {
        playerHP -= rafaelAuraDamage;
        bossHP = maxBossHP;
    }  

    if (ispoisonAttackToBoss)
    {
        bossHP -= rainOfPoisonArrowsTimeDamage + stacksInElectricRay;
    }
    
    if (ispoisonAttackToPlayer)
    {
        playerHP -= rainOfPoisonArrowsTimeDamage;
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