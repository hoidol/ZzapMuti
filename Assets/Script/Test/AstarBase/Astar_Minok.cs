using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector2 point = new Vector2();
    public Vector2 Point
    {
        get { return point; }
        set { point = value; }
    }

    private Vector2 end = new Vector2();
    public Vector2 End
    {
        get { return end; }
        set { end = value; }
    }

    private int f;//비용
    public int F
    {
        get { return f; }
        set { f = value; }
    }
    private int g;//지난 거리
    public int G
    {
        get { return g; }
        set { g = value; }
    }
    private int h;//남은 거리
    public int H
    {
        get { return h; }
        set { h = value; }
    }

    private Node nodeParent;
    public Node NodeParent
    {
        get { return nodeParent; }
        set { nodeParent = value; }
    }

    public Node(int _x,int _y,Node _parent,Vector2 _endPoint)
    {
        point.x = _x;
        point.y = _y;
        nodeParent = _parent;
        end = _endPoint;

        if(nodeParent==null)//부모가 없는 경우
        {
            g = 0;
        }
        else if(//십자 방향일 경우
            (nodeParent.point.x==point.x-1&& nodeParent.point.y == point.y)||
            (nodeParent.point.x == point.x + 1 && nodeParent.point.y == point.y)||
            (nodeParent.point.x == point.x && nodeParent.point.y == point.y-1)||
            (nodeParent.point.x == point.x  && nodeParent.point.y == point.y+1)
            )
        {
            g = nodeParent.G + 10;
        }
        else if (//대각선 방향일 경우
            (nodeParent.point.x == point.x - 1 && nodeParent.point.y == point.y-1) ||
            (nodeParent.point.x == point.x - 1 && nodeParent.point.y == point.y+1) ||
            (nodeParent.point.x == point.x + 1 && nodeParent.point.y == point.y-1) ||
            (nodeParent.point.x == point.x + 1&& nodeParent.point.y == point.y+1)
        )
        {
            G = nodeParent.g + 14;
        }
        else
        {
            Debug.LogError("부모 설정 오류");
            f = -100000; h = -100000; g = -100000;
        }

        h = (int)(Mathf.Abs(end.x - point.x) + Mathf.Abs(end.y - point.y)) * 10;

        f = g + h;
    }
}

public class Map
{
    public int sizeX=5;
    public int sizeY=8;

    public int[,] map=new int[5,8];


    public Map()
    {
        char[] csizeX=new char[5];
        char[] csizeY = new char[8];

        for(int i=0;i<csizeX.Length;i++)
        {
            for(int j=0; j<csizeY.Length;j++)
            {
                map[i, j] = 0;//0은 지나갈 수 있는 곳 1은 장애물
            }
        }
    }

    public void Copy(Map _map)
    {
    }

    public void PrintMap()
    {

    }
}

public class Astar_Minok
{
    private Vector2 startPoint=new Vector2();//출발지점
    private Vector2 endPoint = new Vector2();//목표지점
    private List<Vector2> path=new List<Vector2>();//경로
    public List<Vector2> Path
    {
        get { return path; }
    }

    private Map navi=new Map();
    private Map printMap = new Map();

    public Astar_Minok(Vector2 _StartPoint, Vector2 _EndPoint)
    {
        startPoint = _StartPoint;
        endPoint = _EndPoint;
        FindPath();
    }
    public Astar_Minok()
    {
    }

    //private Func
    private List<Vector2> FindPath(Map _navi,Vector2 _startPoint,Vector2 _endPoint)
    {
        //상하좌우 4방향 시계방향 탐색 후 결과에 따라 대각선 탐색
        List<Node> openNode=new List<Node>();//열린노드
        List<Node> closeNode = new List<Node>();//닫힌노드
        Node sNode;//선택한 노드
        List<Vector2> _path = new List<Vector2>();

        openNode.Add(new Node((int)_startPoint.x, (int)_startPoint.y, null, _endPoint));

        //열린 노드가 비거나(열린 노드 시작==끝) 목적지에 도착(열린 노드에서 값이 발견)한 경우
        
        Node nodeTemp;

        int f = 0;

        while (openNode.Count!=0&& null == FindCoordNode((int)_endPoint.x, (int)_endPoint.y, openNode))
        {
            f++;

            if (f > 999)
            {
                Debug.LogError("While TTT");
                break;
            }

            if (null != FindCoordNode((int)_endPoint.x, (int)_endPoint.y, openNode))
                break;

            sNode = FindNextNode(openNode);

            Debug.Log(_startPoint);
            Debug.Log(endPoint);
            ExploreNode(_navi, sNode, openNode, closeNode, _endPoint);

            closeNode.Add(sNode);
            openNode.Remove(sNode);
        }

        if (openNode.Count > 0) //길을 찾은경우
        {
            Debug.Log("SADDDDDDDDDDDDDDDDDDDD"+ openNode.Count);
            nodeTemp = FindCoordNode((int)_endPoint.x, (int)_endPoint.y, openNode);
            Debug.Log(nodeTemp);
            for(sNode=nodeTemp;sNode.NodeParent!=null;sNode=sNode.NodeParent)
            {
                _path.Add(new Vector2(sNode.Point.x, sNode.Point.y));
            }

            _path.Add(new Vector2(sNode.Point.x, sNode.Point.y));

            _path.Reverse();
            
            return _path;
        }

        return _path;
    }

    private Node FindNextNode(List<Node> _openNode)//오픈 노드 중 F값이 제일 작은 노드를 찾아서 반환
    {
        Node nodeTemp;

        int minValue = 1000000000;
        int order = 0;

        for(int i=0;i<_openNode.Count;i++)
        {
            nodeTemp = _openNode[i];
            if(nodeTemp.F<=minValue)
            {
                minValue = nodeTemp.F;
                order=i;
            }
        }

        return _openNode[order];
    }

    private Node FindCoordNode(int _x,int _y,List<Node> _nodeList)//노드 리스트에서 x,y좌표의 노드를 찾아서 주소를 반환. 없으면 end()반환
    {
        for(int i=0;i< _nodeList.Count;i++)
        {
            if (_nodeList[i].Point.x == _x && _nodeList[i].Point.x == _y)
                return _nodeList[i];
        }
        return null;
    }

    //8방향 노드를 탐색하고 열린 노드에 추가 및 부모 변경을 실행
    private void ExploreNode(Map _navi, Node _sNode,List<Node> _openNode, List<Node> _closeNode,Vector2 _endPoint)
    {
        bool up = true;
        bool right = true;
        bool down = true;
        bool left = true;

        Vector2 point = new Vector2();
        Node nodeTemp;


        //상 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x - 1;
        point.y = _sNode.Point.y;
        Debug.Log(point.x); Debug.Log(point.y);
        if (_sNode.Point.x>0&&_navi.map[(int)point.x, (int)point.y] ==0)
        {
            //장애물이 없는 경우에 장애물 false 세팅
            up = false;

            //이미 열린 노드에 있는 경우
            if(null!=FindCoordNode((int)point.x,(int)point.y,_openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 10)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null!= FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //상방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //상방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                Debug.Log("UP REGST");
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //우 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x;
        point.y = _sNode.Point.y+1;
        if (_sNode.Point.y<_navi.sizeY-1&& _navi.map[(int)point.x, (int)point.y] == 0)
        {
            //장애물이 없는 경우에 장애물 false 세팅
            right = false;


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 10)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //우방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //우방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //하 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x+1;
        point.y = _sNode.Point.y ;
        if (_sNode.Point.x < _navi.sizeX - 1 && _navi.map[(int)point.x, (int)point.y] == 0)
        {
            //장애물이 없는 경우에 장애물 false 세팅
            down = false;


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 10)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //하방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //하방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //좌 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x;
        point.y = _sNode.Point.y-1;
        if (_sNode.Point.y>0 && _navi.map[(int)point.x, (int)point.y] == 0)
        {
            //장애물이 없는 경우에 장애물 false 세팅
            left = false;


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 10)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //하방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //하방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //우상 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x-1;
        point.y = _sNode.Point.y + 1;
        if (_sNode.Point.x > 0&& _sNode.Point.y <_navi.sizeY-1 && _navi.map[(int)point.x, (int)point.y] == 0
            && up == false && right == false)
        {


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 14)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //우상방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //우상방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //우하 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x + 1;
        point.y = _sNode.Point.y + 1;
        if (_sNode.Point.x < _navi.sizeX-1 && _sNode.Point.y < _navi.sizeY - 1 && _navi.map[(int)point.x, (int)point.y] == 0
            && down == false && right == false)
        {


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 14)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //우하방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //우하방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //좌하 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x + 1;
        point.y = _sNode.Point.y - 1;
        if (_sNode.Point.x < _navi.sizeX - 1 && _sNode.Point.y >0 && _navi.map[(int)point.x, (int)point.y] == 0
            && left == false && down == false)
        {


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                    if ((nodeTemp.G > (_sNode.G + 14)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                    {
                        nodeTemp.NodeParent = _sNode;
                    }
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //좌하방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //좌하방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
        //좌상 방향에 맵이 존재, 장애물이 없을 경우
        point.x = _sNode.Point.x - 1;
        point.y = _sNode.Point.y - 1;
        if (_sNode.Point.x >0 && _sNode.Point.y > 0 && _navi.map[(int)point.x, (int)point.y] == 0
            && left == false && up == false)
        {


            //이미 열린 노드에 있는 경우
            if (null != FindCoordNode((int)point.x, (int)point.y, _openNode))
            {
                nodeTemp = FindCoordNode((int)point.x, (int)point.y, _openNode);
                
                if ((nodeTemp.G > (_sNode.G + 14)))//원래 부모를 통해서 갔을 때 비용보다 현재노드를 통해서 갔을때 비용이 더 낮아지는 경우
                {
                    nodeTemp.NodeParent = _sNode;
                }
                
            }

            //닫힌 노드에 있는경우
            else if (null != FindCoordNode((int)point.x, (int)point.y, _closeNode))
            {
            }

            //좌하방향에 장애물이 없고 열린 노드 및 닫힌 노드에 추가되어있지 않은경우
            //좌하방향 노드를 열린 노드에 추가, 부모는 현재 탐색 노드로 지정
            else
            {
                _openNode.Add(new Node((int)point.x, (int)point.y, _sNode, _endPoint));
            }
        }
    }

    //public Func
    public void FindPath()
    {
        path = FindPath(navi, startPoint, endPoint);
    }

    public Vector2 GetPos(int _order)//order번째의 경로의 좌표를 받아옴
    {
        Vector2 pos = new Vector2();

        pos.x = path[_order].x;
        pos.x = path[_order].y;

        return pos;
    }

    public List<Vector2> GetPath()
    {
        return path;
    }
    
    public void SetFree(int _x,int _y)
    {
        navi.map[_x,_y] = 0;
    }

    public void SetObstacle(int _x,int _y)
    {
        navi.map[_x, _y] = 1;
    }

    public void PrintPath() { }

    public void PrintMap() { }

    public void PrintNavi() { }



}
