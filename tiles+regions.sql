use kingdom



Drop Table Regions

Create Table Regions
(
	[Id] Int Primary Key Identity,
	[Row] Int,
	[Col] Int
)

Drop Table Tiles

Create Table Tiles
(
	[Id] Int Primary Key Identity,
	[RegionId] Int,
	[Row] Int,
	[Col] Int,
	[Type] NVarchar(32),
	[Data] NVarchar(max)
)

select * from regions
select * from tiles
select * from tiles Where [Type] = 'Water'