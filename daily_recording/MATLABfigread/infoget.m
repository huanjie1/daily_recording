clear

[a1,colormap,transparency] =imread('000032_2021-06-11_易方达信用债债券A.png');
% imshow(a1,colormap)
a0=a1;
a1(a1==0)=200;
a1(a1==2)=200;
a1(a1==4)=200;
a1(a1~=200 )=0;
% imagesc(a1)

load('numimages')

global nmref;
nmref=numimages;

zuori1=a1(131:138,23:27);
zuori2=a1(131:138,29:33);
zuori3=a1(131:138,35:39);
zuori4=a1(131:138,41:45);
zuori5=a1(131:138,47:51);
zuori6=a1(131:138,53:57);
zuorijingzhi=i2num(zuori1)+0.1*i2num(zuori3)+0.01*i2num(zuori4)+0.001*i2num(zuori5)+0.0001*i2num(zuori6);

% jinrimax1=a1(21:28,23:27);
% jinrimax2=a1(21:28,29:33);
% jinrimax3=a1(21:28,35:39);
% jinrimax4=a1(21:28,41:45);
% jinrimax5=a1(21:28,47:51);
% jinrimax6=a1(21:28,53:57);
% jinriguzhimax=i2num(jinrimax1)+0.1*i2num(jinrimax3)+0.01*i2num(jinrimax4)+0.001*i2num(jinrimax5)+0.0001*i2num(jinrimax6);

jinri1=a1(268:275,76:80);
jinri2=a1(268:275,82:86);
jinri3=a1(268:275,88:92);
jinri4=a1(268:275,94:98);
jinri5=a1(268:275,100:104);
jinri6=a1(268:275,106:110);
jinriguzhi=i2num(jinri1)+0.1*i2num(jinri3)+0.01*i2num(jinri4)+0.001*i2num(jinri5)+0.0001*i2num(jinri6);

zhangfu1=a1(21:28,397:401);
zhangfu2=a1(21:28,403:407);
zhangfu3=a1(21:28,409:413);
zhangfu4=a1(21:28,415:419);
jinrizhangfumax=i2num(zhangfu1)+0.1*i2num(zhangfu3)+0.01*i2num(zhangfu4);

dataregion=a1(28:244,62:390);
% imagesc(dataregion)
th=zeros(1,329);
tl=zeros(1,329);
tf=zeros(1,329);

for idx=1:329
    temp=find(dataregion(:,idx),200);
    tl(idx)=min(temp);
    th(idx)=max(temp);
end
tm=(tl+th)/2;

for idx=1:329
    if idx==1 || idx==329
        tf(idx)=tm(idx);
    else
        if tm(idx)>tm(idx-1) && tm(idx)>tm(idx+1)
            tf(idx)=th(idx);
        else 
            if tm(idx)<tm(idx-1) && tm(idx)<tm(idx+1)
                tf(idx)=tl(idx);
            else
                tf(idx)=tm(idx);
            end
        end
    end
end
% plot(1:329,tl,1:329,th,1:329,tm,1:329,tf)
data=[(109-tf)/109*jinrizhangfumax (jinriguzhi-zuorijingzhi)/zuorijingzhi*100];

plot(data)

    
    












