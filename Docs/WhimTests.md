# Назначение
Приложение служит для тестирования ПВП-системы настольной игры "Блажь".

# Термины
Сражение -- битва между двумя или несколькими персонажами или NPC. Как минимум один персонаж участвует в сражении.
Персонаж -- участник сражения, управляемый игроком. Имеет характеристики.
NPC -- участник сражения, неигровой персонаж, управляемый искуственным интеллектом. Имеет характеристики.
Боец -- персонаж или NPC, участвующий в сражении. Может иметь поддержку.
Поддержка -- увеличение характеристик бойца во время сражения.
Характеристики (статы) -- навыки персонажа, влияющие на ход сражения.
Статистика -- результат сражений. Рассчитывается для каждого персонажа. Статистика учитывает количество побед, среднее здоровье после победы.

# Требования
|№|Требование|Тест|
|1|Приложение должно уметь проводить автоматические сражения персонажей|
|2|Должна поддерживаться возможность выбирать количество проводимых сражений|
|3|Должно поддерживаться считывание характеристик персонажей из csv и xls файлов|
|4|По окончании сражений должна быть выведена статистика|
|5|Должна быть возможность изменять характеристики персонажей|
|6|Должна быть возможность добавить другого персонажа в поддержку персонажу в сражении. Поддержка добавляет 2 удали и 2 склизскости персонажу в сражении|

# Варианты использования
|№|Название|Параметры|Описание|
|1|OneToOneFightsResultQuery|firstCharacter, secondCharacter, fightsCount|Провести сражение между firstCharacter и secondCharacter fightsCount раз и вернуть статистику|
|2|CharactersQuery|filePath|Прочитать файл и взять из него персонажей и их характеристики|
|3|AddCharacterCommand|character|Добавить персонажа в файл|
|4|ChangeCharacterCommand|character|Изменить характеристики персонажа|

# Доменная модель
```plantuml
left to right direction

class Fighter<<AggregateRoot>> {
    + Id: FighterId {I}
    + Character: Character
    + HasSupport: bool
}

class Character {
    + Id: CharacterId {I, R1}
    + Prowess: int
    + Overconfidence: int
    + Slyness: int
    + Flair: int
}

class Statistics<<AggregateRoot>> {
    + StatisticsId: Id {I}
    + FirstFighterStatistics: FirstFighterStatistics
    + SecondFighterStatistics: SecondFighterStatistics
    + CountOfFights: int
}

class FighterStatistics {
    + FighterId: FighterId {I}
    + Victories: int
    + AverageHealth: double
}

Fighter "1" -- "1" Character : R1
Statistics "1" -- "2" FighterStatistics : R2

```
- `R1`: Боец содержит одного персонажа
- `R2`: Статистика содержит две статистики каждого из бойцов

# Стимулы
|№|Название|Параметры|Описание|
|1|CreateCharacter|character|Создать персонажа|
|2|ChangeCharacter|character|Изменить характеристики персонажа|
|3|GetCharacter|id|Получить персонажа|
|4|GetFightResult|firstCharacter, secondCharacter, countOfFights|Получить статистику по сражению персонажей|
|5|GetAllCharacters||Получить всех персонажей|

# Тестовые цепочки
|№|Цепочка|Вывод|Требование|
|1|||
|2|||
|3|||
|4|||
|5|||
|6|||
