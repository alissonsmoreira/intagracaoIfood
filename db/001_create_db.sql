CREATE TABLE [chart-ifood].dbo.Events (
	Id varchar(36) COLLATE Latin1_General_CI_AS NOT NULL,
	OrderId varchar(36) COLLATE Latin1_General_CI_AS NOT NULL,
	Code varchar(5) COLLATE Latin1_General_CI_AS NOT NULL,
	FullCode varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
	Acknowledged bit NOT NULL DEFAULT 0,
	Processed bit NOT NULL DEFAULT 0,
	PRIMARY KEY (ID)
);