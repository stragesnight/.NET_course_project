# Варіанти використання

## Адміністратор + Робітник

### Авторизація/Деавторизація
Процес входу користувача до свого акаунту в базі даних (або ж вихід із нього).
Для входу потрібен логін і пароль, акаунти мають різні права доступу.

### Перегляд існуючих справ
Відображення списку справ у графічному інтерфейсі.
По натисненню на елемент користувач матиме можливість його редагування/видалення.

### Пошук існуючої справи за певними критеріями
Фільтрування списку справ по введеним користувачем критеріям для подальшого його перегляду.

### Синхронізація списку справ
Процес синхронізації списку справ із базою даних. Відбувається або ж збереження локальних змін,
або завантаження нових даних із бази.

### Додавання нової справи
Створення користувачем нової справи і додавання її до списку.
При цьому відбувається запит на додавання до бази даних.

### Редагування існуючої справи
Зміна користувачем значеннь полів існуючої справи у списку.
При цьому відбувається запит на редагування до бази даних.

### Видалення існуючої справи
Видалення користувачем існуючої справи із списку.
При цьому відбувається запит на видалення до бази даних.

## Адміністратор

### Ініціалізація бази даних
Процес розгортання бази даних на сервері: створення таблиць та їх заповнення початковими значеннями.
Відбувається за допомогою спеціального додатка-ініціалізатора.

### Створення нового проекту
Створення користувачем нового проекту та його додавання до списку.
При цьому відбувається запит на додавання до бази даних.

### Редагування існуючого проекту
Зміна користувачем значеннь полів існуючого проекту (зміна списку прив'язаних справ).
При цьому відбувається запит на редагування до бази даних.

### Видалення існуючого проекту
Видалення користувачем існуючого проекту разом зі всіма прив'язаними до нього справ.
При цьому відбувається запит на видалення до бази даних.

