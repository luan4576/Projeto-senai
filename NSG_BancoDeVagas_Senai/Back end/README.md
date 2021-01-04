
# Introdução

Este será o repositório que estará contido os arquivos back-end da solução "Banco de Vagas".

# Conexão com o Banco

Dentro da API, há uma pasta chamada "Contexts", dentro dela existe um arquivo chamado "Context".

Há uma linha com a seguinte configuração

if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlServer("Data Source=SERVIDOR\\SQLEXPRESS; Initial Catalog=BancoDeVagas; Integrated Security=True");
    }

![alt text](https://i.ibb.co/3r5XJFs/svgimg.jpg)

Altere em "Data Source" para o seu servidor interno. 

Caso não seja integrated Security, faça a mudança colocando "user=usuario;pw=senha" para a autenticação. 


# Segunda etapa da conexão manual

Não podemos esquecer do arquivo 'UsuárioRepository.cs' na pasta /repositories/

Altere a informação "SERVIDOR" da string de conexão para aquela que represente o seu servidor local com as propriedades que se julgarem necessárias.

![alt text](https://i.ibb.co/KGCQSnL/svgimg2.jpg)


# CRIANDO O BANCO

Vá na pasta da API (Onde ficam os arquivos e pastas gerais da estrutura)

entre num terminal cmd ou pelo cmd do próprio Visual Studio.

Digite: 

            dotnet ef database update


- A migração começará a ser feita no seu banco de dados.

O processo acima é importante para fazer a criação do banco de dados (é feito um deploy pelo código para a criação do database).

O esquema utilizado é exatamente o mesmo da modelagem. 

Utilize o arquivo DML.sql para popular o seu banco.

