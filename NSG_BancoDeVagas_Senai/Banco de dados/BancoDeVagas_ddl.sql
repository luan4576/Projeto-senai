create database BancoDeVagas 
go

use BancoDeVagas


 create table TipoVaga(
 IdTipoVaga int primary key identity,
 NomeTipoVaga varchar (20)
 )
 go

 create table TipoCurso(
 IdTipoCurso int primary key identity, 
 NomeTipoCurso varchar (50) 
 )
 go

 create table Usuario(
 IdUsuario int primary key identity,
 Email varchar(254) unique not null,
 Senha varchar(16) not null,
 Administrador bit default 0,
 )
 go

 create table Candidato(
 IdCandidato int primary key identity,
 NomeAluno varchar(50),
 Foto nvarchar(max),
 DataNascimento datetime,
 CPF varchar(14) unique,
 IdUsuario int foreign key references Usuario(IdUsuario)
 )
 go

 create table Empresa(
 IdEmpresa int primary key identity,
 RazaoSocial varchar(50) unique,
 NomeFantasia varchar(50) unique,
 CNAE nvarchar(max),
 ImagemEmpresa nvarchar(max), 
 CNPJ varchar(50) unique,
 IdUsuario int foreign key references Usuario(IdUsuario)
 )
 go

 create table Vaga(
 IdVaga int primary key identity,
 Titulo varchar(100),
 Descricao nvarchar(max),
 PalavraChave varchar(50),
 Endereco nvarchar(max),
 Salario int, 
 StatusVaga bit,
 IdEmpresa int foreign key references Empresa(IdEmpresa),
 IdTipoCurso int foreign key references TipoCurso(IdTipoCurso),
 IdTipoVaga int foreign key references TipoVaga(IdTipoVaga)
 )
 go

 create table Curriculo(
 IdCurriculo int primary key identity,
 Cursando bit,
 Linguas nvarchar(max),
 Descricao nvarchar(max),
 Escolaridade nvarchar(max),
 CursosFormacoes nvarchar(max),
 PalavraChave varchar(255),
 IdCandidato int foreign key references Candidato(IdCandidato)
 )
 go

 create table Candidatura(
 IdCandidatura int primary key identity,
 Escolhido bit,
 DataCandidatura datetime,
 IdCandidato int foreign key references Candidato(IdCandidato),
 IdVaga int foreign key references Vaga(IdVaga)
 )
 go

 create table Prazos
 (
    IdPrazo int primary key identity,
	PrazoInicio datetime,
	PrazoFim datetime,
	IdVaga int foreign key references Vaga (IdVaga)
 )
 go

 use master;