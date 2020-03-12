using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector_Bandit : MonoBehaviour {

    [SerializeField] GameObject[] characters;

    private int m_index = 0;
    private int m_maxIndex;
    private GameObject m_currentCharacter;

    // Use this for initialization
    void Start() {
        m_maxIndex = characters.Length;
        m_currentCharacter = Instantiate(characters[0]);
        m_currentCharacter.transform.parent = transform;
    }

    // Update is called once per frame
    void Update() {
        // Change character
        if (Input.GetKeyDown("r")) {
            changeCharacter();  
        }
    }

    private void changeCharacter() {
        // Destroy previous character
        Destroy(m_currentCharacter);

        // Increase index
        m_index++;
        m_index = m_index < m_maxIndex ? m_index : 0;

        // Spawn next character
        m_currentCharacter = Instantiate(characters[m_index]);
        m_currentCharacter.transform.parent = transform;
    }
}
