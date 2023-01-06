using Gat.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
game g = new game();
g.start();
public class game
{
    Cell[,] field = new Cell[13, 13];
    Snake snake;
    private Timer _timer = null;
    Timer _timer4Keys = null;
    dir lastDir;
    public ConsoleKeyInfo k;
    public void fillTheField()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                field[i, j] = new Cell(j,i);
            }
        }
    }
    public void show()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                field[i, j].show(snake);
            }
            Console.WriteLine();
        }
    }
    public void spawnFruit()
    {
        List<int[]> emptyCells = new List<int[]>();
        for (int i = 0; i < 13; i++)
            for (int j = 0; j < 13; j++)
                if (field[i, j].st == state.empty)
                    emptyCells.Add(new int[] { i, j });
        Random random = new Random();
        int ind = random.Next(emptyCells.Count);
        field[emptyCells[ind][0], emptyCells[ind][1]].st = state.fruit;
    }
    public bool anyFruits()
    {
        for (int i = 0; i < 13; i++)
            for (int j = 0; j < 13; j++)
                if (field[i, j].st == state.fruit)
                    return true;
        return false;
    }
    public int[] fruitCoordinate()
    {
        for (int i = 0; i < 13; i++)
            for (int j = 0; j < 13; j++)
                if (field[i, j].st == state.fruit)
                    return new int[2] { i, j };
                
        return null;
    }
    public void start()
    {
        Console.WriteLine("Write 2 start");
        Console.ReadLine();
        Console.Clear();
        snake = new Snake(ref field);
        fillTheField();
        snake.Spawn();
        snake.setSnakeToField();
        spawnFruit();
        show();
        setKey(new Object());
        if (k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.A)
            lastDir = dir.right;
        _timer = new Timer(update, null, 0, 600);
        _timer4Keys= new Timer(setKey, null, 0, 600);
        while (snake.isAlive) {}
        end();
    }
    public void end()
    {
        Console.Clear();
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Game over");
        Console.ReadLine();
    }
    public void check4State()
    {
        for (int i = 0; i < 13; i++)
            for (int j = 0; j < 13; j++)
                field[i, j].check4State(snake);
    }
    public void update(Object o)
    {
        Console.Clear();
        check4State();
        if (!anyFruits())
            spawnFruit();
        show();
        Console.WriteLine(k.Key.ToString());
        if ((k.Key == ConsoleKey.UpArrow || k.Key == ConsoleKey.W)&& lastDir!=dir.bot)
        { snake.move(dir.top); lastDir = dir.top; }
        else if ((k.Key == ConsoleKey.DownArrow || k.Key == ConsoleKey.S)&& lastDir != dir.top)
        { snake.move(dir.bot); lastDir = dir.bot; }
        else if ((k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.A) && lastDir != dir.right)
        {   snake.move(dir.left); lastDir = dir.left; }
        else if ((k.Key == ConsoleKey.RightArrow || k.Key == ConsoleKey.D) && lastDir != dir.left)
        { snake.move(dir.right);  lastDir = dir.right; }
        else snake.move(lastDir);
    }
    public void setKey(object O) {k = Console.ReadKey();  }

}
public class Cell
{
    public state st;
    int x, y;
    public Cell(int x, int y) 
    { 
        st = state.empty;
        this.x = x;
        this.y = y;
    }
    public void show(Snake s)
    {
        if (st == state.empty)
            Console.Write($"[ ]");
        else if (st == state.fruit)
            Console.Write("[*]");
        else if (st == state.snake)
            s.getSnakePartIndex(x, y).show();
        else
            Console.Write($"[{st}]");
    }
    public void check4State(Snake s)
    {
        if (s.getSnakePartIndex(x, y) != null)
            st = state.snake;
        else if (s.getSnakePartIndex(x, y) == null && st != state.fruit)
            st = state.empty;
    }
}
public class Snake
{

    Cell[,] field;
    public bool isAlive;
    List<SnakePart> mySnake = new List<SnakePart>();
    public Snake(ref Cell[,] field) {this.field = field; isAlive = true; }
    public void Spawn()
    {
        mySnake.Add(new SnakePart(dir.right, part.head, 5, 6));
        mySnake.Add(new SnakePart(dir.right, part.body, 4, 6));
        mySnake.Add(new SnakePart(dir.right, part.end, 3, 6));
    }
    public SnakePart getSnakePartIndex(int x, int y)
    {
        for (int i = 0; i < mySnake.Count; i++)
            if (mySnake[i].x==x && mySnake[i].y==y)
                return mySnake[i];
            return null;
    }
    public void move(dir d)
    {
        mySnake[0].p = part.body;
        field[mySnake[mySnake.Count - 1].y, mySnake[mySnake.Count - 1].x].st = state.empty;
        mySnake.RemoveAt(mySnake.Count-1);
        mySnake[mySnake.Count - 1].p = part.end;
        if (d == dir.top) rotTop();
        else if (d == dir.bot) rotBottom();
        else if (d == dir.left) rotLeft();
        else if(d==dir.right) rotRight();
        setSnakeToField();
    }
    bool check4Life(int x, int y)
    {
        if (field[y,x].st == state.snake)
        {
            isAlive = false;
            return false;
        }    
        return true;
    }
    private void rotRight()
    {
        if (mySnake[0].x == 12)
        {
            if (check4Life(0, mySnake[0].y))
                mySnake.Insert(0, new SnakePart(dir.right, part.head, 0, mySnake[0].y));
        }
        else
        {
            if (check4Life(mySnake[0].x + 1, mySnake[0].y))
                mySnake.Insert(0, new SnakePart(dir.right, part.head, mySnake[0].x + 1, mySnake[0].y));
        }
        if (field[mySnake[0].y, mySnake[0].x].st == state.fruit)
            eat(mySnake[0].x, mySnake[0].y, mySnake[0].dir);

    }
    private void rotBottom()
    {
        if (mySnake[0].y < 12)
        {
            if (check4Life(mySnake[0].x, mySnake[0].y + 1))
                mySnake.Insert(0, new SnakePart(dir.bot, part.head, mySnake[0].x, mySnake[0].y + 1));
        }
        else
        {
            if (check4Life(mySnake[0].x, 0))
                mySnake.Insert(0, new SnakePart(dir.bot, part.head, mySnake[0].x, 0));
        }
        if (field[mySnake[0].y, mySnake[0].x].st == state.fruit)
            eat(mySnake[0].x, mySnake[0].y, mySnake[0].dir);
    }
    private void rotLeft()
    {
        if (mySnake[0].x != 0)
        {
            if (check4Life(mySnake[0].x - 1, mySnake[0].y))
                mySnake.Insert(0, new SnakePart(dir.left, part.head, mySnake[0].x - 1, mySnake[0].y));
        }
        else
        {
            if (check4Life(12, mySnake[0].y))
                mySnake.Insert(0, new SnakePart(dir.left, part.head, 12, mySnake[0].y));
        }
        if (field[mySnake[0].y, mySnake[0].x].st == state.fruit)
            eat(mySnake[0].x, mySnake[0].y, mySnake[0].dir);
    }
    private void rotTop()
    {
        if (mySnake[0].y > 0)
        {
            if (check4Life(12, mySnake[0].y))
                mySnake.Insert(0, new SnakePart(dir.top, part.head, mySnake[0].x, mySnake[0].y - 1));
        }
        else
        {
            if (check4Life(mySnake[0].x, 12))
                mySnake.Insert(0, new SnakePart(dir.top, part.head, mySnake[0].x, 12));
        }
        if (field[mySnake[0].y, mySnake[0].x].st == state.fruit)
            eat(mySnake[0].x, mySnake[0].y, mySnake[0].dir);
    }
    private void eat(int x, int y, dir d)
    {
        mySnake[0].p = part.body;
        if (d == dir.top) rotTop();
        else if (d == dir.bot) rotBottom();
        else if (d == dir.left) rotLeft();
        else if (d == dir.right) rotRight();
    }
    public void setSnakeToField()
    {
        for (int i = 0; i < mySnake.Count; i++)
            if (mySnake[i]!=null)
                field[mySnake[i].y, mySnake[i].x].st = state.snake;
    }
}
public class SnakePart
{
    public int x, y;
    public dir dir; //куда напрвлeна
    public part p;
    public SnakePart( dir d, int x, int y)
    {
        dir = d;
        p = part.body;
        this.x = x;
        this.y = y;
    }
    public SnakePart(dir d, part p, int x, int y)
    {
        dir = d;
        this.p = p;
        this.x = x;
        this.y = y;
    }
    public void show()
    {
        if (p == part.head)
            showHead(dir);
        else if (p == part.end)
            showEnd(dir);
        else
            showBody();

    }
    public void showBody()
    {
        if (dir == dir.top || dir == dir.bot)
            Console.Write(" ║");
        else if (dir == dir.left || dir == dir.right)
            Console.Write(" = ");
        else
            Console.Write(dir);
    }
    public void showHead(dir d)
    {
        if (d == dir.right)
            Console.Write(" ► ");
        else if (d == dir.left)
            Console.Write(" ◄ ");
        else if (d == dir.top)
            Console.Write(" ▲");
        else
            Console.Write(" ▼ ");
    }
    public void showEnd(dir d)
    {
        Console.Write(" █ ");
    }
}
public enum part { body, head, end }
public enum dir { top, bot, right, left }
public enum state { snake, fruit, empty }