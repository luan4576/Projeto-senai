use BancoDeVagas;

insert into TipoVaga(NomeTipoVaga)
values
('CLT'),

('PJ'),

('Estágio');

go

insert into TipoCurso(NomeTipoCurso)
values
('Desenvolvimento de sistemas'),

('Redes de computadores'),

('Informática para internet')

go

insert into Usuario(Email, Senha, Administrador)
values
('claudia.amauribello@hotmail.com', 'claudia123', 0),

('edumanerao@gmail.com', 'edu123', 0),

('JOAO.ALVES9598@OUTLOOK.COM', 'JOAO123', 0),

('lucasnogueira_silva@yahoo.com.br', 'lucas123', 0),

('matheusfsc01@yahoo.com.br', 'matheus123', 0),

('janpacheco@ig.com.br', 'jan123', 0),

('Luanafoontes@gmail.com', 'Luana123', 0),

('LUCASSRINALDINI@YAHOO.COM.BR', 'LUCAS123', 0),

('MURILORPINTO@GMAIL.COM', 'MURILO123', 0),

('thiago_consta@hotmail.com', 'thiago123', 0),

('viniciusfgs@outlook.com', 'vinicius123', 0),

('TONY.SOUSA09@GMAIL.COM', 'TONY123', 0),

('EMILI_MARIA_LEITE@HOTMAIL.COM', 'EMILI123', 0),

('reginacaff@hotmail.com', 'regina123', 0),

('tiagomolero@gmail.com', 'tiago123', 0),

('ORTIZLUIZ@YAHOO.COM', 'ORTIZ123', 0),

('vinicios2508@gmail.com', 'vinicius250', 0),

('Joao.matheus2819@gmail.com', 'Joao123', 0),

('adilsonsanchesjunior555@hotmail.com', 'adilson123', 0),

('EDSGONCALVES7@GMAIL.COM', 'EDS123', 0),

('SILVIO.PAIXAO@TERRA.COM.BR', 'SILVIO123', 0),

('HENRI.MIGUEL@HOTMAIL.COM', 'HENRI123', 0),

('joaodias_spider@hotmail.com', 'joaodias123', 0),

('julianabrito.s@outlook.com', 'juliana123', 0),

('KAUE.CL.15@GMAIL.COM', 'KAUE123', 0),

('lukavigotto@hotmail.com', 'luka123', 0),

('P.MOISES@UOL.COM.BR', 'P123', 0),

('monicacongregacao@gmail.com', 'monica123', 0),

('biancaqueiroz.s@hotmail.com', 'bianca123', 0),

('giovana_grandepresentededeus@hotmail.com', 'giovana123', 0),

('robertoPossarle@gmail.com', 'possarle123', 0),

('caique@gmail.com', 'caique123', 0),

('carrefour@gmail.com', 'carrefour123', 0),

('brq@gmail.com', 'brq123', 0),

('space@gmail.com', 'space123', 0)

go

insert into Candidato(NomeAluno, Foto, DataNascimento, CPF, IdUsuario)
values
('Claudia Amauri Bello', '', '01-01-2000 01:00:00', '239.084.300-03', 1),

('Eduardo Medeiros', '', '02-02-2000 02:00:00', '002.161.100-92', 2),

('João Alves', '', '03-03-2000 03:00:00', '855.707.990-79', 3),

('Lucas Nogueira', '', '04-04-2000 04:00:00', '040.821.850-97', 4),

('Matheus Figueira', '', '05-05-2000 05:00:00', '273.207.350-40', 5),

('Jan Pacheco', '', '06-06-2000 06:00:00', '907.326.350-63', 6),

('Luana Fontes', '', '07-07-2000 07:00:00', '350.266.290-83', 7),

('Lucas Rinaldi', '', '08-08-2000 08:00:00', '499.700.300-00', 8),

('Murilo Pinto', '', '09-09-2000 09:00:00', '557.475.530-03', 9),

('Thiago Constancio', '', '10-10-2000 10:00:00', '328.699.440-55', 10),

('Vincius Fagundes', '', '11-11-2000 11:00:00', '932.242.540-76', 11),

('Tony Souza', '', '12-12-2000 12:00:00', '020.035.380-20', 12),

('Emili Maria', '', '13-01-2000 13:00:00', '916.426.070-45', 13),

('Regina Castro', '', '14-02-2000 14:00:00', '979.385.730-78', 14),

('Tiago Molero', '', '15-03-2000 15:00:00', '938.610.800-36', 15),

('Ortiz Luiz', '', '16-04-2000 16:00:00', '255.878.640-73', 16),

('Vinicius Silva', '', '17-05-2000 17:00:00', '318.480.320-13', 17),

('João Matheus', '', '18-06-2000 18:00:00', '288.955.920-34', 18),

('Adilson Sanches', '', '19-07-2000 19:00:00', '052.967.060-72', 19),

('Edson Goncalves', '', '20-08-2000 20:00:00', '937.037.080-34', 20),

('Silvio Paixao', '', '21-09-2000 21:00:00', '994.613.340-79', 21),

('Henri Miguel', '', '22-10-2000 22:00:00', '051.066.920-40', 22),

('João Dias', '', '23-11-2000 01:00:00', '689.671.350-09', 23),

('Juliana Brito', '', '24-12-2000 02:00:00', '283.997.390-14', 24),

('Kaue Costa', '', '25-01-2000 03:00:00', '807.340.160-69', 25),

('Lucas Vitor', '', '26-02-2000 04:00:00', '193.486.150-25', 26),

('Paulo Moises', '', '27-03-2000 05:00:00', '153.378.410-84', 27),

('Monica Congregação', '', '28-04-2000 06:00:00', '494.772.480-37', 28),

('Bianca Queiroz', '', '29-05-2000 07:00:00', '936.444.410-81', 29),

('Giovana Queiroz de Oliveira', '', '30-06-2000 08:00:00', '843.602.880-50', 30)

go

insert into Empresa(RazaoSocial, NomeFantasia, CNAE, CNPJ, IdUsuario)
values
('Carrefour S.A.', 'Carrefour', 'G47', '45.543.915/0001-81', 33),

('BRQ SOLUCOES EM INFORMATICA S.A.', 'BRQ', 'M71', '36.542.025/0001-64', 34),

('Space Needle Tecnologia', 'Space', 'M71', '18.381.466/0001-40', 35)

go

insert into Vaga(Titulo, Descricao, PalavraChave, Endereco, Salario, StatusVaga, IdEmpresa, IdTipoCurso, IdTipoVaga)
values
('Analista de desenvolvimento pleno', 'Atuará como Analista de Desenvolvimento de Sistema Pleno, codificando requisitos de back-end e testes de primeiro nível.', 'C#', 'Rua dos anjos, 120', 3500, 1, 1, 1, 1),

('Programador Java', 'Ressalta-se a oportunidade de aprendizado e especialização em uma tecnologia emergente e altamente promissora.', 'Java', 'Rua Alpiringa', 3600, 1, 2, 1, 2),

('Analista de redes', 'Monitoramento de rede, performance e banco.', 'MSSQL Server', 'Morumbi', 4000, 1, 3, 2, 3),

('Analista de redes e equipamentos', 'Monitoramento de equipamentos e ativos de redes.', 'Grafana', 'Rua Conceição', 3000, 1, 1, 2, 1),

('Consultar de novas tecnologias', 'Conceitualização e implantação de soluções tecnológicas avançadas para problemas complexos que requerem um conhecimento elevado e específico dos negócios dos clientes', 'Big Data', 'Rua Gopouva', 2800, 1, 2, 3, 2),

('Atendente de suporte técnico', 'Prestar assistência nos processos da área administrativa, rotina de lançamentos dos mapas estatísticos e de estoque no sistema, elaboração de planilhas e relatórios, afim de cumprir o que o gestor solicita.', 'WMS', 'Rua Pimentas', 3000, 1, 3, 3, 3);

go

insert into Curriculo(Cursando, Linguas, Descricao, Escolaridade, CursosFormacoes, PalavraChave, IdCandidato)
values
(1, 'Inglês', 'Jovem a procura de uma oportunidade de emprego.', 'Ensino médio completo', 'Nenhum', 'Java', 1),

(1, 'Inglês e alemão', 'A procura da primeira experiência de trabalho.', 'Cursando o ensino médio', 'Excel avançado', 'SQL', 2),

(1, 'Inglês e chinês', 'Anseando por ampliar meus conhecimentos.', 'Cursando ensino superior', 'Mecânica de usinagem', 'C#', 3),

(1, 'Inglês', 'Procurando emprego.', 'Cursando ensino médio', 'Pronatec', 'React', 4),

(1, 'Não fala nenhuma lingua estrangeira', 'Procurando uma oportunidade de estágio.', 'Ensino médio completo', 'Técnico em informática', 'Xamarim', 5)

go

insert into Candidatura(Escolhido, DataCandidatura, IdCandidato, IdVaga)
values
(0, '12-04-2020 13:00:00', 1, 1),

(1, '06-10-2020 15:00:00', 2, 2),

(0, '07-06-2020 20:00:00', 3, 1)

go

insert into Prazos(PrazoInicio, PrazoFim, IdVaga)
values
('04-05-2020 12:00:00', '07-07-2020 16:00:00', 1),

('05-05-2020 14:00:00', '08-08-2020 17:00:00', 2),

('03-03-2020 11:00:00', '10-10-2020 19:00:00', 3);