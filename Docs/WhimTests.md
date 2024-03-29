# Назначение

Приложение служит для тестирования ПВП-системы настольной игры "Блажь".

# Термины

Сражение -- битва между двумя или несколькими персонажами или NPC. Как минимум один персонаж участвует в сражении.
Игровой персонаж -- участник сражения, управляемый игроком. Имеет характеристики. Может иметь поддержку.
NPC -- участник сражения, неигровой персонаж, управляемый искуственным интеллектом. Имеет характеристики.
Персонаж -- игровой персонаж или NPC.
Поддержка -- увеличение характеристик игрового персонажа во время сражения.
Боец -- персонаж, участвующий в сражении.
Характеристики (статы) -- навыки персонажа, влияющие на ход сражения. Удаль (УДА), Склизскость (СКЛ), Самонадеянность (САМ), Чуйка (ЧУЙ).
Статистика -- результат сражений. Рассчитывается для каждого персонажа. Статистика учитывает количество побед, среднее здоровье после победы.
Кубик -- элемент случайности в сражении. nДk -- результат "броска" кубика, выраженный в числовом значении, в котором n -- количество бросков кубика, значения которого могут быть от 1 до k.

# Требования

| №   | Требование                                                                                                                                                                              | Тест                |
| :-- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------------ |
| R1  | Приложение должно уметь проводить автоматические сражения персонажей                                                                                                                    | Явно не тестируется |
| R2  | Должна поддерживаться возможность выбирать количество проводимых сражений                                                                                                               | S1                  |
| R3  | Должно поддерживаться считывание характеристик персонажей из csv и xls файлов                                                                                                           | S2                  |
| R4  | По окончании сражений должна быть выведена статистика                                                                                                                                   | Явно не тестируется |
| R5  | Должна быть возможность изменять характеристики персонажа                                                                                                                               | S3                  |
| R6  | Должна быть возможность добавить другого игрового персонажа в поддержку игровому персонажу в сражении. Поддержка добавляет 1Д6+2 удали, 1Д6+2 склизскости и +10 ХП персонажу в сражении | S4                  |
| R7  | Должна быть возможность создать нового персонажа                                                                                                                                        | S5                  |
| R8  | Должна быть возможность получить всех персонажей                                                                                                                                        | S6                  |

# Характеристики

- Удаль (УДА) - Prowess
- Склизскость (СКЛ) - Slyness
- Самонадеянность (САМ) - Overconfidence
- Чуйка (ЧУЙ) - Flair

# Правила сражения

В сражение вступают 2 бойца. Бойцы по очереди наносят друг другу удары, пока один из них не проиграет.
Очерёдность хода: нападающий проверяет свою САМ + 2Д6, обороняющийся првоеряет ЧУЙ + 2Д6. Чей результат больше, тот наносит удар первым.
При ударе атакующий проверяет свою УДА + 2Д6. Защищающийся проверяет свою СКЛ + 2Д6. Если значение атакующего больше, то ХП защищающегося уменьшается на разница между значениями. Если значение атакующего меньше, то ничего не происходит.
Критерии поражения. Для NPC -- когда ХП падает до 0 или ниже. Для Персонажа -- когда ХП падает до 5 или ниже.

Игровой персонаж может иметь поддержку.

# Варианты использования

| №   | Название               | Параметры                       | Описание                                                                         |
| :-- | :--------------------- | :------------------------------ | :------------------------------------------------------------------------------- |
| 1   | StatisticsQuery        | attacker, defender, fightsCount | Провести сражение между attacker и defender fightsCount раз и вернуть статистику |
| 2   | CharactersQuery        | filePath                        | Прочитать файл и взять из него персонажей и их характеристики                    |
| 3   | CreateCharacterCommand | character                       | Добавить персонажа в файл                                                        |

# Доменная модель

```plantuml

class File<<AggregateRoot>> {
    Path: string {I}
    Characters: Character[]
}

class Fighter {
     Id: FighterId {I}
     CharacterId: CharacterId
}

 abstract class Character {
     Id: CharacterId {I}
     Prowess: int
     Overconfidence: int
     Slyness: int
     Flair: int
}

class PlayerCharacter {
     Id: PlayerCharacterId {I}
     Name: string
     Prowess: int
     Overconfidence: int
     Slyness: int
     Flair: int
     HasSupport: bool
}

class NPC {
     Id: NPCId {I}
     Name: string
     Prowess: int
     Overconfidence: int
     Slyness: int
     Flair: int
}

class Statistics<<AggregateRoot>> {
     Id: StatisticsId {I}
     AttackerStatistics: FighterStatistics
     DefenderStatistics: FighterStatistics
     CountOfFights: int
}

class FighterStatistics {
     Id: FighterId {I}
     Victories: int
     AverageHealth: double
}

File "1" -- "*" Character
Character -- PlayerCharacter
Character -- NPC
Fighter "*" .. "1" Character
Statistics "1" -- "2" FighterStatistics
FighterStatistics "1" -- "1" Fighter


```

# Тестирование

Из-за случайных значений, выкидываемых кубиком, усложняется тестирование программы. Для облегчения тестирования было принято решение заменить случайные значения на заранее определённые.

# Стимулы

| №   | Название         | Параметры                         | Описание                                   |
| :-- | :--------------- | :-------------------------------- | :----------------------------------------- |
| 1   | SaveCharacter    | character                         | Создать персонажа                          |
| 2   | ChangeCharacter  | character                         | Изменить характеристики персонажа          |
| 3   | GetCharacter     | id                                | Получить персонажа                         |
| 4   | StatisticsQuery  | attacker, defender, countOfFights | Получить статистику по сражению персонажей |
| 5   | GetAllCharacters |                                   | Получить всех персонажей                   |

# Тестовые цепочки

| №   | Цепочка | Описание                                                                                                                                                                                                                                                                | Требование |
| :-- | :------ | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :--------- |
| 1   | S1      | Создаются 2 персонажа;Проводится заданное количество сражений;В статистике сумма количества побед персонажей == заданному количеству сражений                                                                                                                           | R2         |
| 2   | S2      | Создать персонажа;Сохранить его;Достать из файла персонажа с тем же id; Персонажи должны быть одинаковыми                                                                                                                                                               | R3         |
| 3   | S3      | Создать персонажа;Изменить его храрактеристики;Достать персонажа из файла;Характеристики должны быть изменены                                                                                                                                                           | R5         |
| 4   | S4      | Создать персонажа 1 с УДА и СКЛ = 5 и ХП = 1; Создать второго персонажа с УДА и СКЛ = 6 и ХП = 1; Второй персонаж бьёт первым; Кубик всегда возвращает одинаковое значение; 2 тест-кейса: 1) Персонаж 1 без поддержки проигрывает 2) Персонаж 1 с поддержкой выигрывает | R6         |
| 5   | S5      | создать 2 персонажей; сохранить их; получить всех персонажей; полученные персонажи должны быть равны созданным                                                                                                                                                          |
