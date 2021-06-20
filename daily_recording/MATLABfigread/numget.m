clear

[a1,colormap,transparency] =imread('004229.png');
% imshow(a1,colormap)
a1(a1==0)=200;
a1(a1==2)=200;
a1(a1==4)=200;
a1(a1~=200 )=0;
imagesc(a1)

numimage1=a1(104:111,409:413);
numimage2=a1(21:28,415:419);
numimage3=a1(49:56,409:413);
numimage4=a1(213:220,47:51);
numimage5=a1(21:28,409:413);
numimage6=a1(76:83,415:419);
numimage7=a1(158:165,47:51);
numimage8=a1(131:138,47:51);
numimage9=a1(49:56,415:419);
numimage10=a1(21:28,397:401);

numimages=zeros(8,5,10);
for idx=1:10
    eval(['numtemp=numimage' num2str(idx) ';']);
    numtemp=double(numtemp);
    numimages(:,:,idx)=numtemp/sqrt(sum(sum(numtemp.^2)));
%     eval(['numimages(:,:,idx)=numimage' num2str(idx) ';']);
end


save('numimages','numimages');





