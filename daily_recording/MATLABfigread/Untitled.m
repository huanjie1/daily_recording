clear

[a1,colormap,transparency] =imread('004229.png');
% imshow(a1,colormap)

zuori1=a1(131:138,23:27);
zuori2=a1(131:138,29:33);
zuori3=a1(131:138,35:39);
zuori4=a1(131:138,41:45);
zuori5=a1(131:138,47:51);
zuori6=a1(131:138,53:57);

jinri1=a1(21:28,397:401);
jinri2=a1(21:28,403:407);
jinri3=a1(21:28,409:413);
jinri4=a1(21:28,415:419);

dataregion=a1(28:244,62:390);


a1black=a1;
a1black(a1black~=0)=5;
figure;imagesc(a1black)
a1red=a1;
a1red(a1red~=2)=5;
figure;imagesc(a1red)
a1green=a1;
a1green(a1green~=4)=5;
figure;imagesc(a1green)

%black,0
%red,2
%greem,2

d1green=dataregion;
d1green(d1green~=4)=5;
figure;imagesc(d1green)





