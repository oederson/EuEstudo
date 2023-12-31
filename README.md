# Eu estudo 

O **Eu Estudo** é uma aplicação web desenvolvida em ASP.NET MVC com o propósito de auxiliar estudantes em suas jornadas de aprendizado. A plataforma oferece recursos para o cadastro de usuários, permitindo que cada usuário registre suas disciplinas, associadas a questões e respostas relacionadas aos seus estudos.

Para a gestão de autenticação e autorização de usuários, a aplicação faz uso do framework Identity. A persistência de dados é efetuada por meio do Entity Framework, sendo que o banco de dados escolhido é o SQL Server.

A interface do **Eu Estudo** é construída com Razor Pages, oferecendo uma experiência amigável e de fácil navegação para os usuários. Além disso, a aplicação é acompanhada de testes de controle (controller) que empregam as tecnologias XUnit e Moq. Para garantir um ambiente isolado e reprodutível durante os testes, é utilizado um banco de dados in-memory.

O projeto visa proporcionar uma plataforma confiável e eficaz para aqueles que desejam aprimorar seus estudos, oferecendo recursos de organização, acesso rápido a informações relevantes e autenticação segura.

## Testes de integração
Os testes foram implementados utilizando XUnit e moq.