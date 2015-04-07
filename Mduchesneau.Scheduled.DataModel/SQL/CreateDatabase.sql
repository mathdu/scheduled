
use scheduled;

-- TABLES
create table [Calendar](
	[ID] int identity(1,1) primary key,
	[Name] nvarchar(100) not null,
	[Created] datetime not null
);

create table [ScheduleEvent](
	[ID] int identity(1,1) primary key,
	[CalendarID] int not null,
	[Title] nvarchar(100) not null,
	[Start] datetime not null,
	[End] datetime not null,
	[Created] datetime not null
);

-- KEYS
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleEvent_Calendar] FOREIGN KEY([CalendarID]) REFERENCES [dbo].[Calendar] ([ID])
GO
ALTER TABLE [dbo].[ScheduleEvent] CHECK CONSTRAINT [FK_ScheduleEvent_Calendar]
GO

