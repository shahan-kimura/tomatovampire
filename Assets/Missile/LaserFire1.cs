using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire1 : MonoBehaviour
{
    Vector3 acceleration; // ���[�U�[�̉����x
    Vector3 velocity; // ���[�U�[�̑��x
    Vector3 position; // ���[�U�[�̈ʒu
    Transform target; // ���[�U�[�̃^�[�Q�b�g

    [SerializeField][Tooltip("���e����")] float period = 1f; // ���[�U�[���^�[�Q�b�g�ɓ��B���鎞��
    [SerializeField][Tooltip("���e����")] float deltaPeriod = 0.5f; // ���e���Ԃ̃����_���ȕϓ��͈�
    [SerializeField][Tooltip("x������")] float x_initial_v = 10f; // X�������̏����x
    [SerializeField][Tooltip("y������")] float y_initial_v = 10f; // Y�������̏����x
    [SerializeField][Tooltip("z������")] float z_initial_v = 10f; // Z�������̏����x

    void Start()
    {
        // �^�O "Enemy" �����I�u�W�F�N�g��Transform���^�[�Q�b�g�ɐݒ�
        target = GameObject.FindWithTag("Enemy").GetComponent<Transform>();

        // ���[�U�[�̏����ʒu��ݒ�
        position = transform.position;

        // ���[�U�[�̏������x�������_���ɐݒ�A�n�ʂ߂荞�݋֎~��y���̂�0�ȏ��
        velocity = new Vector3(Random.Range(-x_initial_v, x_initial_v),
                                Random.Range(0, y_initial_v),
                                Random.Range(-z_initial_v, z_initial_v));

        // ���e���Ԃ������_���ɕϓ�������
        period += Random.Range(-deltaPeriod, deltaPeriod);
    }

    void Update()
    {
        //�^���������Ft�b�Ԃɐi�ދ���(diff) = (�����x(v) * t) �{ (1/2 *�����x(a) * t^2)
        //�ό`�����
        //�^���������F�����x(a) = 2*(diff - (v * t)) / t^2 
        //�Ȃ̂ŁA�u���xv�̕��̂�t�b���diff�i�ނ��߂̉����xa�v���Z�o�ł���
        //GameObject��v�͎擾�ł��邵�At���擾�ł���
        //�Ȃ�A���[�U�[��period�b��ɓ����idiff��0�j���邽�߂ɕK�v��a���Z�o�ł���

        acceleration = Vector3.zero; // ���������x��0�ɐݒ�

        Vector3 diff = target.position - position; // �^�[�Q�b�g�Ƃ̋������v�Z

        // �K�v�ȉ����x���v�Z
        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime; // �c��̊��Ԃ�����������

        // period��0�����ɂȂ����ꍇ�A�I�u�W�F�N�g��j��
        if (period < 0f)
        {
            Destroy(gameObject);
            return;
        }

        // ���݂̑��x���X�V
        velocity += acceleration * Time.deltaTime;

        // ���݂̈ʒu���X�V
        position += velocity * Time.deltaTime;

        // �I�u�W�F�N�g�̈ʒu���X�V
        transform.position = position;
    }
}
