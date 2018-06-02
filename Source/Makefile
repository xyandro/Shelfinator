OBJS = DotStar.o Driver.o Pattern.o

all: shelfinator

shelfinator: $(OBJS)
	g++ -o $@ $^

%.o: %.cpp %.h
	g++ -c -o $@ $<

clean:
	rm -f shelfinator $(OBJS)
