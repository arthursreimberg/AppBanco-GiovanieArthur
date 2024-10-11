create database dbBancoApp2;
use dbBancoApp2;

create table cliente (
IdCli int primary key auto_increment,
nomeCli varchar(200),
CPF varchar(11),
RG varchar(9),
DataNasCli date
);

insert into cliente(nomeCli, CPF, RG, DataNasCli) values ("Arthur", "12345678912","123456789",'2007/10/26');

select * from cliente;