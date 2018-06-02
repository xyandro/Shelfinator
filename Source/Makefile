OBJS = DotStar.o Driver.o DriverRunner.o Pattern.o Remote.o

all: shelfinator

shelfinator: $(OBJS)
	g++ -o $@ $^ -llirc_client -pthread

%.o: %.cpp %.h
	g++ -c -o $@ $<

clean:
	rm -f shelfinator $(OBJS)
