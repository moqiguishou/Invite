--local file = io.open("./test.lua")
--local tLine = {}
--for line in file:lines() do
--	--for i = 1, string.len(line) do
--		--print(string.sub(line,i,i))
--	--end	
--	local str = "\""..line.."\" & vbCr & _"
--	table.insert(tLine,str)
--end
--io.close(file)
--local file2 = io.open("./Out.lua","w+b")
--for i,v in pairs(tLine) do
--	if file2:write(v.."\n") == nil then 
--		return false 
--	end
--end
--io.close(file2)

local t = {1,2,3,4}
table.remove(t,2)

for i,v in pairs(t) do
	print(i.." "..v)
end