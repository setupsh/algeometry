import time
i = 0
reversed = False
deltatime = 1/60
while True:
   if reversed:
      i -= 1
   else: 
      i += 1
   
   if i > 200:
      reversed = True
   if i <= 0:
      reversed = False

   time.sleep(deltatime)
   print(" "*i + "I HATE NIGGERS")
   clear()
