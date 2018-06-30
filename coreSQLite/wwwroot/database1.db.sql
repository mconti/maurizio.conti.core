BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Spesa` (
	`Id`	TEXT,
	`NomeProprietario`	TEXT,
	`Data`	TEXT,
	`Importo`	REAL,
	`Descrizione`	TEXT,
	PRIMARY KEY(`Id`)
);
CREATE TABLE IF NOT EXISTS `Padre` (
	`Username`	TEXT,
	`Password`	TEXT,
	PRIMARY KEY(`Username`)
);
CREATE TABLE IF NOT EXISTS `Figlio` (
	`Username`	TEXT,
	`Password`	TEXT,
	`NomePadre`	TEXT,
	PRIMARY KEY(`Username`)
);
COMMIT;
