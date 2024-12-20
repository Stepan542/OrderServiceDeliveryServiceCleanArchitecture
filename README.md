# Order + Delivery Service

Данный проект, представляет из себя связь между двумя микросервисами. Один из них создаёт заказ, другой из них - принимает данные созданного заказа. 

# Stack

**ASP.NET Core** - используется для создания веб-API обоих микросервисов — OrderService и DeliveryService.

**Clean Architecture** - разбивает проект на Domain, Application, Infrastructure, API.

**RabbitMQ** - используется для асинхронной коммуникации между OrderService и DeliveryService.

**PostgreSQL** - база данных для хранения заказов в каждом сервисе.

**Entity Framework Core** - используется для работы с PostgreSQL, что упрощает манипуляции с данными в базе.

**AutoMapper** - применяется для преобразования моделей данных между слоями, например, для маппинга Order на OrderDto.

**Ocelot** - реализован API Gateway, который управляет маршрутизацией запросов к микросервисам Order и Delivery, упрощая доступ к ним и добавляя возможности маршрутизации.

**Dependency Injection (DI)** - активно используется для управления зависимостями и повышения модульности кода.