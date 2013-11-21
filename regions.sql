USE Kingdom

Go

Drop Table Regions

Go

create table Regions (
  Id integer identity Primary Key not null, -- this may be unnecessary 
  RegionId	Int Not Null,
  [Row]		smallint not null, -- 0 to 1327
  [Col]		smallint not null, -- 0 to 582
  [TileRow] tinyint  not null, -- 0 to 15
  [TileCol] tinyint  not null, -- 0 to 15
  [Type]    nvarchar(16) not null,
  [Data]	nvarchar(max),
  [Building] nvarchar(max),
  [Units] nvarchar(max)
)

Declare @RegionCounter Int
Declare @RegionCounter2 Int

Declare @Counter Int
Declare @End Int
Declare @Counter2 Int
Declare @End2 Int

Set @RegionCounter = 0
Set @Counter = 0
Set @Counter2 = 0
Set @End = 10
Set @End2 = 10

Declare @Id Int
Set @Id = 1

While @RegionCounter < 10
Begin

	Set @RegionCounter2 = 0

	While @RegionCounter2 < 10
	Begin
		Set @Counter = 0
		While @Counter < @End
		Begin

			Set @Counter2 = 0

			While @Counter2 < @End2
			Begin
				Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type], [Data]) Values (@Id, @RegionCounter, @RegionCounter2, @Counter, @Counter2, 'Forest', '{"$type":"Kingdom.Core.Sql.Entities.Tiles.Grass, Kingdom.Core.Sql","Position":{"$type":"Kingdom.Core.Sql.Entities.Position, Kingdom.Core.Sql","X":'+Convert(nvarchar(12), @Counter) +',"Y":'+ Convert(nvarchar(12), @Counter2) +'},"Type":4}')
				Set @Counter2 = @Counter2 + 1
			End
			Set @Counter = @Counter + 1
		End
		Set @RegionCounter2 = @RegionCounter2 + 1
		Set @Id = @Id + 1
	End
	Set @RegionCounter = @RegionCounter + 1
End



select * from Regions
select * from Regions Where regionid = 1

Update Regions Set Data = null

--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 0, 0, 'Grass')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 0, 1, 'Forest')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 0, 2, 'Grass')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 1, 0, 'Grass')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 1, 1, 'Forest')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 1, 2, 'Forest')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 2, 0, 'Grass')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 2, 1, 'Grass')
--Insert Into Regions (RegionId, [Row], [Col], [TileRow], [TileCol], [Type]) Values (1, 0, 0, 2, 2, 'Grass')