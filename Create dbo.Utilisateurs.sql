USE [SoutenanceDb]
GO

/****** Objet : Table [dbo].[Utilisateurs] Date du script : 19/06/2023 23:08:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Utilisateurs] (
    [Id]                         INT            IDENTITY (1, 1) NOT NULL,
    [Username]                   NVARCHAR (30)  NOT NULL,
    [PasswordHash]               NVARCHAR (MAX) NOT NULL,
    [PasswordSalt]               NVARCHAR (MAX) NOT NULL,
    [RefreshToken]               NVARCHAR (MAX) NOT NULL,
    [RefreshTokenTime]           DATETIME       NOT NULL,
    [IsEditPassword]             BIT            NOT NULL,
    [IsConnected]                BIT            NOT NULL,
    [IsSuperAdmin]               BIT            NOT NULL,
    [IsAdmin]                    BIT            NOT NULL,
    [DateExpirationCompte]       NVARCHAR (30)  NOT NULL,
    [Statut]                     INT            NOT NULL,
    [DateDesactivation]          NVARCHAR (MAX) NOT NULL,
    [DateAjout]                  NVARCHAR (MAX) NOT NULL,
    [DateModificationMotDePasse] NVARCHAR (MAX) NOT NULL
);


