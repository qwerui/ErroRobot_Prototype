Behavior Tree
베이스 코드 출처 : https://www.youtube.com/watch?v=nKpM98I7PeM&t=832s

폴더 이동 시 Editor폴더 하위의 BehaviorTreeEditor.cs, NodeView.cs, BehaviorTreeView.cs의 경로를 수정하세요

사용법
1. 우클릭 시 나오는 AI에서 BehaviorTree 생성
2. 생성된 BehaviorTree를 더블클릭하면 에디터가 활성화된다.
3. 에디터의 우측 화면에 우클릭 시 나오는 드롭다운에서 노드 추가
4. 노드의 연결점을 연결
5. 오브젝트에 BehaviorTreeRunner 추가
6. BehaviorTreeRunner에 BehaviorTree 바인딩

블랙보드 사용법
1. Blackboard를 상속 받은 클래스 생성
2. CreateAssetMenu 속성 추가
3. 필드를 만들고 생성자에 Set<T>를 사용해 바인딩
4. 에셋 생성
5. BehaviorTree에 Blackboard 바인딩 

기본으로 만든 노드
1. 대기
2. Selector, Sequencer
3. 루프