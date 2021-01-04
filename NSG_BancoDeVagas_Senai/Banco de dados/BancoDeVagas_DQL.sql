use BancoDeVagas;
go

select  IdCandidatura, Vaga.Titulo, Candidato.NomeAluno from Candidatura 
 inner join Vaga on Candidatura.IdCandidatura = Vaga.IdVaga
 inner join Candidato on Candidatura.IdCandidatura = Candidato.IdCandidato
 go
 

 select IdEstagio, Vaga.IdVaga, Vaga.Titulo, Candidato.IdCandidato, Candidato.NomeAluno, DataInicio, DataFim from Estagio
inner join Vaga on Estagio.IdEstagio = Vaga.IdVaga
inner join Candidato on Estagio.IdEstagio = Candidato.IdCandidato
go


select * from Vaga where IdTipoCurso = 1
go

select * from Vaga

