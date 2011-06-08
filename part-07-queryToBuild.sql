/*
select * from Orchard_Tags_TagRecord
select * from Orchard_Tags_TagsPartRecord
select * from Orchard_Tags_ContentTagRecord
select * from Routable_RoutePartRecord
select * from Common_CommonPartVersionRecord
select * from Orchard_Framework_ContentItemRecord where Id=8
select * from Orchard_Framework_ContentItemVersionRecord where ContentItemREcord_Id=8
*/
select distinct top(5)
	i.Id,
	rp.Title,
	cm.PublishedUtc
from
	Orchard_Framework_ContentItemRecord i
	inner join Orchard_Framework_ContentItemVersionRecord v on i.Id=v.ContentItemRecord_Id and v.Published=1
	inner join Common_CommonPartVersionRecord cm on v.Id = cm.Id
	inner join Routable_RoutePartRecord rp on rp.Id=v.Id
	inner join Orchard_Tags_ContentTagRecord ctag on ctag.TagsPartRecord_Id=i.ID
	inner join Orchard_Tags_TagRecord tag on ctag.TagRecord_Id=tag.Id
where tag.TagName in ('home', 'about', 'cat2')
order by cm.PublishedUtc desc

select distinct
	i.Id
from
	Orchard_Framework_ContentItemRecord i
	inner join Orchard_Framework_ContentItemVersionRecord v on i.Id=v.ContentItemRecord_Id and v.Published=1
	inner join Orchard_Tags_ContentTagRecord ctag on ctag.TagsPartRecord_Id=i.ID
	inner join Orchard_Tags_TagRecord tag on ctag.TagRecord_Id=tag.Id
where tag.TagName in ('home', 'about', 'cat2')

