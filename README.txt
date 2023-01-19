This implementation was made in Unity engine version 2021.3.13f1
and should only require the engine to run, with no extra features needed.

If the map with pre-made water and floating objects is not loaded when 
opening the project, open the 'Playground' in Assets


Wave Manager - 
Using the wave manager of each object you can set the parameters of the waves. If the water is
set to using the shared material, you should use the FIRST PLACED water instance using the
shared material to set the parameters, as any other water instances set after this will have 
their parameters overriden by this one on game start, even if they seem to change beforehand. 

Therefore, it is recommened you keep track of the order you place water instances in. 
Toggling the first placed instance to a unique material will result in the second placed 
instance overriding shared material parameters, and so on, unless every material is unique, 
or an earlier placed material is toggled back to shared.

Changing specific material parameters such as water colour can be done within the water 
surface, at the material inputs at the bottom, however these water material parameters will
apply to all water, even if instanced, and instanced materials will be overridden by 
shared material parameters.


Buoyancy - 
To make a buoyant object, add the 'Object Buoyancy' script to an object with a mesh, collider, 
and rigidbody. To assign the object's float-points- create empty game objects, placing them at 
the corners or significant areas of the object. Make them children of your floating object, so
they'll move with it.
Then add these float points to the array in the Object Buoyancy script. If the object is small,
or you don't need the increased accuracy of the float points, you can simply put the 
object's own transform into this array and leave it at that.




Credits -
Mesh from Low Poly Water Pack by Ebru Dogan on the Unity Asset store - 
Available at: https://assetstore.unity.com/packages/tools/particles-effects/lowpoly-water-107563

Caustics texture from Abode Substance 3D Designer examples -
Available at: https://substance3d.adobe.com/documentation/sddoc/version-11-2-215286709.html

Other textures avaliable from Ben Cloward youtube - specfically water material tutorials -
Available at: https://www.youtube.com/@BenCloward
