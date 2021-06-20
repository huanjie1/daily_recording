function numout = i2num(image0)

global nmref

inputimage=double(image0);
inputimage=inputimage/sqrt(sum(sum(inputimage.^2)));
xcorvalue=zeros(1,10);
for idx=1:10
    xcorvalue(idx)=sum(sum(inputimage.*nmref(:,:,idx)));
%     xcorvalue(idx)=sum(sum(inputimage.*inputimage));
end
[aa,numout]=max(xcorvalue);

if numout==10
    numout=0;
end


end

